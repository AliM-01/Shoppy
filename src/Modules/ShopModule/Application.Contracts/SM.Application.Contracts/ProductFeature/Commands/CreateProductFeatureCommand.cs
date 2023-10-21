using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Commands;

public record CreateProductFeatureCommand
    (CreateProductFeatureDto ProductFeature) : IRequest<ApiResult>;