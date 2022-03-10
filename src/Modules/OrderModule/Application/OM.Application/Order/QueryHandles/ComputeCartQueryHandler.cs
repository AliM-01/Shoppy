using _0_Framework.Infrastructure;
using DM.Domain.ProductDiscount;
using IM.Application.Contracts.Inventory.Helpers;
using IM.Domain.Inventory;
using MongoDB.Driver;
using SM.Domain.Product;

namespace OM.Application.Order.QueryHandles;

public class ComputeCartQueryHandler : IRequestHandler<ComputeCartQuery, Response<CartDto>>
{
    #region Ctor

    private readonly IGenericRepository<Inventory> _inventoryRepository;
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IGenericRepository<ProductDiscount> _productDiscountRepository;
    private readonly IInventoryHelper _inventoryHelper;
    private readonly IMapper _mapper;

    public ComputeCartQueryHandler(IGenericRepository<Product> productRepository,
                                                    IGenericRepository<Inventory> inventoryRepository,
                                                    IGenericRepository<ProductDiscount> productDiscountRepository,
                                                    IInventoryHelper inventoryHelper,
                                                    IMapper mapper)
    {
        _inventoryRepository = Guard.Against.Null(inventoryRepository, nameof(_inventoryRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
        _inventoryHelper = Guard.Against.Null(inventoryHelper, nameof(_inventoryHelper));
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<CartDto>> Handle(ComputeCartQuery request, CancellationToken cancellationToken)
    {
        var cart = new CartDto();
        cart.Items = new List<CartItemDto>();

        var productDiscounts = await _productDiscountRepository
                            .AsQueryable()
                            .Where(x => !x.IsExpired)
                            .Select(x => new { x.Rate, x.ProductId })
                            .ToListAsyncSafe();

        foreach (var cartItem in request.Items)
        {
            if (await _inventoryRepository.ExistsAsync(x => x.ProductId == cartItem.ProductId))
            {
                #region check inventory

                var filter = Builders<Inventory>.Filter.Eq(x => x.ProductId, cartItem.ProductId);

                var itemInventory = await _inventoryRepository.GetByFilter(filter);

                if (itemInventory is null) return new Response<CartDto>(cart);

                var product = await _productRepository.GetByIdAsync(itemInventory.ProductId);

                var itemToReturn = new CartItemDto
                {
                    Count = cartItem.Count,
                    Title = product.Title,
                    ProductId = cartItem.ProductId,
                    UnitPrice = itemInventory.UnitPrice,
                    ImagePath = product.ImagePath,
                    ImageAlt = product.ImageAlt,
                    ImageTitle = product.ImageTitle
                };

                itemToReturn.CalculateTotalItemPrice();

                itemToReturn.IsInStock = (await _inventoryHelper.CalculateCurrentCount(itemInventory.Id)) >= cartItem.Count;


                #endregion

                #region discount

                var productDiscount = productDiscounts.FirstOrDefault(x => x.ProductId == cartItem.ProductId);

                if (productDiscount is not null)
                    itemToReturn.DiscountRate = productDiscount.Rate;

                itemToReturn.DiscountAmount = ((itemToReturn.TotalItemPrice * itemToReturn.DiscountRate) / 100);
                itemToReturn.ItemPayAmount = itemToReturn.TotalItemPrice - itemToReturn.DiscountAmount;

                #endregion

                cart.Items.Add(itemToReturn);
            }
        }

        for (int i = 0; i < cart.Items.Count; i++)
        {
            cart.TotalAmount += cart.Items[i].TotalItemPrice;
            cart.PayAmount += cart.Items[i].ItemPayAmount;
            cart.DiscountAmount += cart.Items[i].DiscountAmount;
        }

        return new Response<CartDto>(cart);
    }
}
