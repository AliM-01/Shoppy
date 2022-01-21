using SM.Application.Contracts.ProductFeature.DTOs;

namespace SM.Application.Contracts.ProductFeature.Commands;

public record EditProductFeatureCommand
    (EditProductFeatureDto ProductFeature) : IRequest<Response<string>>;