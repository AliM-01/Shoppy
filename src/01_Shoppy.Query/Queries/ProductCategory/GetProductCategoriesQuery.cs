using _01_Shoppy.Query.Helpers.Product;
using _01_Shoppy.Query.Models.ProductCategory;
using AutoMapper;
using SM.Infrastructure.Persistence.Context;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoriesQuery() : IRequest<Response<IEnumerable<ProductCategoryQueryModel>>>;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, Response<IEnumerable<ProductCategoryQueryModel>>>
{
    #region Ctor

    private readonly ShopDbContext _context;
    private readonly IMapper _mapper;

    public GetProductCategoriesQueryHandler(ShopDbContext context,
        IProductHelper productHelper, IMapper mapper)
    {
        _context = Guard.Against.Null(context, nameof(_context));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductCategoryQueryModel>>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategories = await _context.ProductCategories
             .Select(productCategory => _mapper.Map(productCategory, new ProductCategoryQueryModel()))
             .ToListAsync();

        return new Response<IEnumerable<ProductCategoryQueryModel>>(productCategories);
    }
}
