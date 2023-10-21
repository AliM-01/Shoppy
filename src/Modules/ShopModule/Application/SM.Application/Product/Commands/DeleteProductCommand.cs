using _0_Framework.Domain.Validators;
using FluentValidation;

namespace SM.Application.ProductCategory.CommandHandles;

public record DeleteProductCommand(string ProductId) : IRequest<ApiResult>;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(p => p.ProductId)
            .RequiredValidator("شناسه");
    }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult>
{
    private readonly IRepository<Domain.Product.Product> _productRepository;

    public DeleteProductCommandHandler(IRepository<Domain.Product.Product> productRepository)
    {
        _productRepository = Guard.Against.Null(productRepository, nameof(_productRepository));
    }

    public async Task<ApiResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.ProductId);

        NotFoundApiException.ThrowIfNull(product);

        await _productRepository.DeleteAsync(product.Id);

        return ApiResponse.Success(ApplicationErrorMessage.RecordDeleted);
    }
}