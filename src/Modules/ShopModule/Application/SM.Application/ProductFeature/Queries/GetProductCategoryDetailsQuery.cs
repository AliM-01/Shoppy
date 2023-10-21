using FluentValidation;
using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Queries;

public record GetProductFeatureDetailsQuery(string Id) : IRequest<EditProductFeatureDto>;

public class GetProductFeatureDetailsQueryValidator : AbstractValidator<GetProductFeatureDetailsQuery>
{
    public GetProductFeatureDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .RequiredValidator("شناسه");
    }
}


public class GetProductFeatureDetailsQueryHandler : IRequestHandler<GetProductFeatureDetailsQuery, EditProductFeatureDto>
{
    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public GetProductFeatureDetailsQueryHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<EditProductFeatureDto> Handle(GetProductFeatureDetailsQuery request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.Id);

        NotFoundApiException.ThrowIfNull(productFeature);

        return _mapper.Map<EditProductFeatureDto>(productFeature);
    }
}