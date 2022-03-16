using SM.Application.Contracts.ProductCategory.DTOs;
using SM.Application.Contracts.ProductCategory.Queries;

namespace SM.Application.ProductCategory.QueryHandles;
public class GetProductCategoryDetailsQueryHandler : IRequestHandler<GetProductCategoryDetailsQuery, Response<EditProductCategoryDto>>
{
    #region Ctor

    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoryDetailsQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    #endregion

    public async Task<Response<EditProductCategoryDto>> Handle(GetProductCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.GetByIdAsync(request.Id);

        if (productCategory is null)
            throw new NotFoundApiException();

        var mappedProductCategory = _mapper.Map<EditProductCategoryDto>(productCategory);

        return new Response<EditProductCategoryDto>(mappedProductCategory);
    }
}