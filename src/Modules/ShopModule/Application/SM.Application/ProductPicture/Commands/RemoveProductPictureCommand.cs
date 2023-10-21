using FluentValidation;

namespace SM.Application.ProductPicture.Commands;

public record RemoveProductPictureCommand(string ProductPictureId) : IRequest<ApiResult>;

public class RemoveProductPictureCommandValidator : AbstractValidator<RemoveProductPictureCommand>
{
    public RemoveProductPictureCommandValidator()
    {
        RuleFor(p => p.ProductPictureId)
            .RequiredValidator("شناسه");
    }
}

public class RemoveProductPictureCommandHandler : IRequestHandler<RemoveProductPictureCommand, ApiResult>
{
    private readonly IRepository<Domain.ProductPicture.ProductPicture> _productPictureRepository;

    public RemoveProductPictureCommandHandler(IRepository<Domain.ProductPicture.ProductPicture> productPictureRepository)
    {
        _productPictureRepository = Guard.Against.Null(productPictureRepository, nameof(_productPictureRepository));
    }

    public async Task<ApiResult> Handle(RemoveProductPictureCommand request, CancellationToken cancellationToken)
    {
        var productPicture = await _productPictureRepository.FindByIdAsync(request.ProductPictureId);

        NotFoundApiException.ThrowIfNull(productPicture);

        File.Delete(PathExtension.ProductPictureImage + productPicture.ImagePath);
        File.Delete(PathExtension.ProductPictureThumbnailImage + productPicture.ImagePath);

        await _productPictureRepository.DeletePermanentAsync(productPicture.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}