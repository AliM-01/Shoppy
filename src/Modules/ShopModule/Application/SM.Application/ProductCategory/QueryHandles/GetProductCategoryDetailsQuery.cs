using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.ProductCategory.DTOs;
using SM.Application.ProductCategory.Queries;

namespace SM.Application.ProductCategory.Queries;

public record GetProductCategoryDetailsQuery(string Id) : IRequest<EditProductCategoryDto>;

public class GetProductCategoryDetailsQueryValidator : AbstractValidator<GetProductCategoryDetailsQuery>
{
    public GetProductCategoryDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}
public class GetProductCategoryDetailsQueryHandler : IRequestHandler<GetProductCategoryDetailsQuery, EditProductCategoryDto>
{
    private readonly IRepository<Domain.ProductCategory.ProductCategory> _productCategoryRepository;
    private readonly IMapper _mapper;

    public GetProductCategoryDetailsQueryHandler(IRepository<Domain.ProductCategory.ProductCategory> productCategoryRepository, IMapper mapper)
    {
        _productCategoryRepository = Guard.Against.Null(productCategoryRepository, nameof(_productCategoryRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditProductCategoryDto> Handle(GetProductCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _productCategoryRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(productCategory);

        return _mapper.Map<EditProductCategoryDto>(productCategory);
    }
}