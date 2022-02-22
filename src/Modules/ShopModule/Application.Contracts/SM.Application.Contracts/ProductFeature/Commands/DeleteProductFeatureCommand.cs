namespace SM.Application.Contracts.ProductFeature.Commands;

public record DeleteProductFeatureCommand
    (string ProductFeatureId) : IRequest<Response<string>>;