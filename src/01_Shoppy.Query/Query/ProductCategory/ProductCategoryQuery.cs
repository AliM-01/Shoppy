using _01_Shoppy.Query.Contracts.ProductCategory;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IMapper _mapper;

    public ProductCategoryQuery(ShopDbContext context, IMapper mapper)
    {
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductQueryModel>>> GetProductCategories()
    {
        var productCategories = await _context.ProductCategories
            .Select(productCategory => _mapper.Map(productCategory, new ProductQueryModel()))
            .ToListAsync();

        return new Response<IEnumerable<ProductQueryModel>>(productCategories);
    }
}
