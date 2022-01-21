namespace SM.Application.Contracts.ProductFeature.Commands;

public record DeleteProductFeatureCommand
    (long ProductFeatureId) : IRequest<Response<string>>;