using _0_Framework.Application.Exceptions;
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

    public async Task<Response<ProductCategoryQueryModel>> GetProductCategoryWithProductsBy(ProductCategoryDetailsFilterModel filter)
    {
        if (filter.CategoryId == 0 && string.IsNullOrEmpty(filter.Slug))
            throw new NotFoundApiException();

        var categories = await _context.ProductCategories.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        bool existsCategory = false;
        long existsCategoryId = 0;


        if (filter.CategoryId != 0 && (string.IsNullOrEmpty(filter.Slug) || !string.IsNullOrEmpty(filter.Slug)))
        {
            var category = categories.FirstOrDefault(x => x.Id == filter.CategoryId);
            if (category is not null)
            {
                existsCategoryId = category.Id;
                existsCategory = true;
            }
        }
        if (filter.CategoryId == 0 && !string.IsNullOrEmpty(filter.Slug))
        {
            var category = categories.FirstOrDefault(x => x.Slug == filter.Slug);
            if (category is not null)
            {
                existsCategoryId = category.Id;
                existsCategory = true;
            }
        }

        if (!existsCategory)
            throw new NotFoundApiException();

        var productCategory = await _context.ProductCategories
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == existsCategoryId);

        var productCategoryProducts = productCategory.Products.ToList();

        var mappedProductCategory = _mapper.Map(productCategory, new ProductCategoryQueryModel());

        var mappedProductCategoryProducts = new List<ProductQueryModel>();

        productCategoryProducts.ForEach(p =>
        {
            var mappedProduct = _productHelper.MapProductsFromProductCategories(p).Result;
            mappedProductCategoryProducts.Add(mappedProduct);
        });

        mappedProductCategory.Products = mappedProductCategoryProducts;

        return new Response<ProductCategoryQueryModel>(mappedProductCategory);
    }
}
