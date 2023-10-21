using SM.Application.ProductCategory.DTOs;
using SM.Application.ProductCategory.Queries;

namespace SM.Application.ProductCategory.QueryHandles;
public class GetProductCategoryDetailsQueryHandler : IRequestHandler<GetProductCategoryDetailsQuery, EditProductCategoryDto>
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

    public async Task<EditProductCategoryDto> Handle(GetProductCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(productCategory);

        return _mapper.Map<EditProductCategoryDto>(productCategory);
    }
}