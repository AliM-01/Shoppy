using DM.Domain.DiscountCode;
using DM.Domain.ProductDiscount;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using Microsoft.AspNetCore.WebUtilities;
using MongoDB.Driver;
using SM.Domain.Product;
using System.Text;

namespace OM.Application.Order.Queries;

public record CheckoutCartQuery(List<CartItemInCookieDto> Items, string? DiscountCodeId) : IRequest<CartDto>;

public class CheckoutCartQueryHandler : IRequestHandler<CheckoutCartQuery, CartDto>
{
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductDiscount> _productDiscountRepository;
    private readonly IRepository<DiscountCode> _discountCodeRepository;
    private readonly IInventoryHelper _inventoryHelper;
    private readonly IMapper _mapper;

    public CheckoutCartQueryHandler(IRepository<Product> productRepository,
                                                    IRepository<Inventory> inventoryRepository,
                                                    IRepository<ProductDiscount> productDiscountRepository,
                                                    IRepository<DiscountCode> discountCodeRepository,
                                                    IInventoryHelper inventoryHelper,
                                                    IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _discountCodeRepository = Guard.Against.Null(discountCodeRepository, nameof(_discountCodeRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<CartDto> Handle(CheckoutCartQuery request, CancellationToken cancellationToken)
    {
        var cart = new CartDto();

        var productDiscounts = (await _productDiscountRepository
                            .FindAsync(x => !x.IsExpired))
                            .ToList()
                            .Select(x => new { x.Rate, x.ProductId })
                            .ToList();

        foreach (var cartItem in request.Items)
        {
            if (cartItem.Count <= 0)
                continue;

            var itemToReturn = new CartItemDto();

            if (!(await _inventoryRepository.ExistsAsync(x => x.ProductId == cartItem.ProductId)))
                continue;

            #region check inventory

            var filter = Builders<Inventory>.Filter.Eq(x => x.ProductId, cartItem.ProductId);

            var itemInventory = await _inventoryRepository.FindOne(filter);

            if (itemInventory is null)
                continue;

            if (!(await _inventoryHelper.IsInStock(itemInventory?.Id)))
                continue;

            long inventoryCount = await _inventoryHelper.CalculateCurrentCount(itemInventory?.Id);

            if (inventoryCount <= 0)
                continue;

            if ((inventoryCount < cartItem.Count))
                continue;

            var product = await _productRepository.FindByIdAsync(itemInventory?.ProductId);

            itemToReturn.UnitPrice = itemInventory.UnitPrice;

            _mapper.Map(product, itemToReturn);
            _mapper.Map(cartItem, itemToReturn);

            itemToReturn.CalculateTotalItemPrice();

            #endregion

            #region discount

            var productDiscount = productDiscounts.FirstOrDefault(x => x.ProductId == cartItem.ProductId);

            if (productDiscount is not null)
            {
                itemToReturn.DiscountRate = productDiscount.Rate;
                itemToReturn.UnitPriceWithDiscount = itemToReturn.UnitPrice - ((itemToReturn.UnitPrice * itemToReturn.DiscountRate) / 100);
            }
            else
            {
                itemToReturn.UnitPriceWithDiscount = itemToReturn.UnitPrice;
            }

            itemToReturn.DiscountAmount = ((itemToReturn.TotalItemPrice * itemToReturn.DiscountRate) / 100);
            itemToReturn.ItemPayAmount = itemToReturn.TotalItemPrice - itemToReturn.DiscountAmount;

            #endregion

            cart.Items.Add(itemToReturn);
        }

        for (int i = 0; i < cart.Items.Count; i++)
        {
            cart.TotalAmount += cart.Items[i].TotalItemPrice;
            cart.PayAmount += cart.Items[i].ItemPayAmount;
            cart.DiscountAmount += cart.Items[i].DiscountAmount;
        }

        #region discount

        if (!string.IsNullOrEmpty(request.DiscountCodeId))
        {
            string decodedDiscountId = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.DiscountCodeId));

            var discountCode = await _discountCodeRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == decodedDiscountId);

            if (discountCode is not null)
            {
                if (!discountCode.IsExpired)
                {
                    decimal discountAmount = (cart.PayAmount * discountCode.Rate) / 100;
                    cart.PayAmount -= discountAmount;
                    cart.DiscountAmount += discountAmount;
                }

            }
        }

        #endregion

        return cart;
    }
}
