using _0_Framework.Application.Exceptions;
using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.Product;
using _01_Shoppy.Query.Models.ProductCategory;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoryWithProductsByQuery(ProductCategoryDetailsFilterModel Filter) : IRequest<Response<ProductCategoryQueryModel>>;

public class GetProductCategoryWithProductsByQueryHandler : IRequestHandler<GetProductCategoryWithProductsByQuery, Response<ProductCategoryQueryModel>>
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IProductHelper _productHelper;
    private readonly IMapper _mapper;

    public GetProductCategoryWithProductsByQueryHandler(ShopDbContext context,
        IProductHelper productHelper, IMapper mapper)
    {
        _productHelper = Guard.Against.Null(productHelper, nameof(_productHelper));
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<ProductCategoryQueryModel>> Handle(GetProductCategoryWithProductsByQuery request, CancellationToken cancellationToken)
    {
        if (request.Filter.CategoryId == 0 && string.IsNullOrEmpty(request.Filter.Slug))
            throw new NotFoundApiException();

        var categories = await _context.ProductCategories.Select(x => new
        {
            x.Slug,
            x.Id
        }).ToListAsync();

        bool existsCategory = false;
        long existsCategoryId = 0;


        if (request.Filter.CategoryId != 0 && (string.IsNullOrEmpty(request.Filter.Slug) || !string.IsNullOrEmpty(request.Filter.Slug)))
        {
            var category = categories.FirstOrDefault(x => x.Id == request.Filter.CategoryId);
            if (category is not null)
            {
                existsCategoryId = category.Id;
                existsCategory = true;
            }
        }
        if (request.Filter.CategoryId == 0 && !string.IsNullOrEmpty(request.Filter.Slug))
        {
            var category = categories.FirstOrDefault(x => x.Slug == request.Filter.Slug);
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
