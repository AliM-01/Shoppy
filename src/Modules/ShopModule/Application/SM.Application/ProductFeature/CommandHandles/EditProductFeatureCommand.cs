using FluentValidation;
using SM.Application.ProductFeature.Commands;
using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Commands;

public record EditProductFeatureCommand(EditProductFeatureDto ProductFeature) : IRequest<ApiResult>;

public class EditProductFeatureCommandValidator : AbstractValidator<EditProductFeatureCommand>
{
    public EditProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeature.Id)
            .RequiredValidator("شناسه");

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

public class EditProductFeatureCommandHandler : IRequestHandler<EditProductFeatureCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;
    private readonly IMapper _mapper;

    public EditProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository, IMapper mapper)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
        _mapper = Guard.Against.Null(mapper, nameof(_mapper));
    }

    public async Task<ApiResult> Handle(EditProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.ProductFeature.Id);

        NotFoundApiException.ThrowIfNull(productFeature);

        if (await _productFeatureRepository.ExistsAsync(x => x.ProductId == request.ProductFeature.ProductId
                                                             && x.FeatureTitle == request.ProductFeature.FeatureTitle
                                                             && x.Id != request.ProductFeature.Id))
            throw new ApiException(ApplicationErrorMessage.DuplicatedRecordExists);

        _mapper.Map(request.ProductFeature, productFeature);

        await _productFeatureRepository.UpdateAsync(productFeature);

        return ApiResponse.Success();
    }
}