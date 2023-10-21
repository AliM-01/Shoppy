using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Commands;

public record EditProductFeatureCommand
    (EditProductFeatureDto ProductFeature) : IRequest<ApiResult>;