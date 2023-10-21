using FluentValidation;
using SM.Application.ProductFeature.Commands;

namespace SM.Application.ProductFeature.Commands;

public record DeleteProductFeatureCommand(string ProductFeatureId) : IRequest<ApiResult>;

public class DeleteProductFeatureCommandValidator : AbstractValidator<DeleteProductFeatureCommand>
{
    public DeleteProductFeatureCommandValidator()
    {
        RuleFor(p => p.ProductFeatureId)
            .RequiredValidator("شناسه دسته بندی");
    }
}

public class DeleteProductFeatureCommandHandler : IRequestHandler<DeleteProductFeatureCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductFeature.ProductFeature> _productFeatureRepository;

    public DeleteProductFeatureCommandHandler(IRepository<Domain.ProductFeature.ProductFeature> productFeatureRepository)
    {
        _productFeatureRepository = Guard.Against.Null(productFeatureRepository, nameof(_productFeatureRepository));
    }

    public async Task<ApiResult> Handle(DeleteProductFeatureCommand request, CancellationToken cancellationToken)
    {
        var productFeature = await _productFeatureRepository.FindByIdAsync(request.ProductFeatureId);

        NotFoundApiException.ThrowIfNull(productFeature);

        await _productFeatureRepository.DeletePermanentAsync(productFeature.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}