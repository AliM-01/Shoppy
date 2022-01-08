using _01_Shoppy.Query.Contracts.ProductCategory;
using _01_Shoppy.Query.Helpers.Product;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Query;

public class ProductCategoryQuery : IProductCategoryQuery
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public ProductCategoryQuery(ShopDbContext context,
        IProductHelper productHelper, IMapper mapper)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductCategoryQueryModel>>> GetProductCategories()
    {
        var productCategories = await _context.ProductCategories
            .Select(productCategory => _mapper.Map(productCategory, new ProductCategoryQueryModel()))
            .ToListAsync();

        return new Response<IEnumerable<ProductCategoryQueryModel>>(productCategories);
    }

    public async Task<Response<IEnumerable<ProductCategoryQueryModel>>> GetProductCategoriesWithProducts()
    {
        var productCategories = await _context.ProductCategories
            .Include(x => x.Products)
            .ToListAsync();

        List<ProductCategoryQueryModel> returnData = new();

        productCategories.ForEach(category =>
        {
            var mappedCategory = _mapper.Map(category, new ProductCategoryQueryModel());
            mappedCategory.Products = _productHelper.MapProductsFromProductCategories(category.Products.ToList()).Result;

            returnData.Add(mappedCategory);
        });

        return new Response<IEnumerable<ProductCategoryQueryModel>>(returnData);
    }
}
