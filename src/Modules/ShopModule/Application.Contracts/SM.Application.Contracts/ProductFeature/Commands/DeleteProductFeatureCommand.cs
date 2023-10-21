namespace SM.Application.ProductFeature.Commands;

public record DeleteProductFeatureCommand
    (string ProductFeatureId) : IRequest<ApiResult>;