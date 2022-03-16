using _0_Framework.Infrastructure;
using _0_Framework.Infrastructure.Helpers;
using _01_Shoppy.Query.Models.ProductCategory;
using AutoMapper;

namespace _01_Shoppy.Query.Queries.ProductCategory;

public record GetProductCategoriesQuery() : IRequest<Response<IEnumerable<ProductCategoryQueryModel>>>;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, Response<IEnumerable<ProductCategoryQueryModel>>>
{
    #region Ctor

    private readonly IRepository<SM.Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoriesQueryHandler(IRepository<SM.Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<IEnumerable<ProductCategoryQueryModel>>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var productCategories = (await _productCategoryRepository.AsQueryable().ToListAsyncSafe())
             .Select(productCategory => _mapper.Map(productCategory, new ProductCategoryQueryModel()))
             .ToList();

        return new Response<IEnumerable<ProductCategoryQueryModel>>(productCategories);
    }
}
