using _0_Framework.Infrastructure.IRepository;
using Ardalis.GuardClauses;
using DM.Domain.ProductDiscount;
using SM.Application.Services;

namespace SM.Infrastructure.AclServices;

public class ProductDiscountAclService : IProductDiscountAclService
{
    private readonly IRepository<ProductDiscount> _productDiscount;

    public ProductDiscountAclService(IRepository<ProductDiscount> productDiscount)
    {
        _productDiscount = Guard.Against.Null(productDiscount, nameof(_productDiscount));
    }

    public List<string> GetHotestDiscountProductIds()
    {
        return _productDiscount.AsQueryable()
            .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            .Where(x => x.Rate >= 25)
            .Take(6).ToList()
            .Select(x => x.ProductId).ToList();
    }
}
