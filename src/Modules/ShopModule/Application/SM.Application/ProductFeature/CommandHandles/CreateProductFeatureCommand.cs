using _0_Framework.Domain.Validators;
using FluentValidation;
using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Commands;

public record CreateProductFeatureCommand(CreateProductFeatureDto ProductFeature) : IRequest<ApiResult>;

public class CreateProductFeatureCommandValidator : AbstractValidator<CreateProductFeatureCommand>
{
    public CreateProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeature.ProductId)
            .RequiredValidator("شناسه محصول");

        RuleFor(p => p.ProductFeature.FeatureTitle)
            .RequiredValidator("عنوان")
            .MaxLengthValidator("عنوان", 100);

        RuleFor(p => p.ProductFeature.FeatureValue)
            .RequiredValidator("توضیحات")
            .MaxLengthValidator("توضیحات", 250);
    }
}

public class CreateProductFeatureCommandHandler : IRequestHandler<CreateProductFeatureCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public CreateProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(CreateProductFeatureCommand request, CancellationToken cancellationToken)
    {
        if (await _productFeatureRepository.ExistsAsync(x => x.ProductId == request.ProductFeature.ProductId
                && x.FeatureTitle == request.ProductFeature.FeatureTitle))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        var productFeature =
            _mapper.Map(request.ProductFeature, new Domain.ProductFeature.ProductFeature());

        await _productFeatureRepository.InsertAsync(productFeature);

        return ApiResponse.Success();
    }
}