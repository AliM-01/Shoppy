using _0_Framework.Application.Exceptions;
using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using DM.Application.Contracts.Sevices;
using SM.Domain.Product;

namespace DM.Infrastructure.ProductAcl.Services;
public class DMProucAclService : IDMProucAclService
{
    #region ctor

    private readonly IRepository<Domain.ProductDiscount.ProductDiscount> _productDiscountRepository;
    private readonly IRepository<Product> _productRepository;

    public DMProucAclService(IRepository<Domain.ProductDiscount.ProductDiscount> productDiscountRepository,
         IRepository<Product> productRepository)
    {
        _productDiscountRepository = Guard.Against.Null(productDiscountRepository, nameof(_productDiscountRepository));
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));

    }

    #endregion

    public async Task<bool> ExistsProductDiscount(string productId)
    {
        var existsProduct = await _productRepository.ExistsAsync(p => p.Id == productId);

        if (!existsProduct)
            throw new NotFoundApiException("محصولی با این شناسه پیدا نشد");

        bool existsDiscount = await _productDiscountRepository.ExistsAsync(x => x.ProductId == productId);

        return existsDiscount;
    }
}
