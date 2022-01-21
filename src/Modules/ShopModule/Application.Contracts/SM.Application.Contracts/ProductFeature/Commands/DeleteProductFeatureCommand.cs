namespace SM.Application.Contracts.ProductFeature.Commands;

public record DeleteProductFeatureCommand
    (long productFeatureId) : IRequest<Response<string>>;