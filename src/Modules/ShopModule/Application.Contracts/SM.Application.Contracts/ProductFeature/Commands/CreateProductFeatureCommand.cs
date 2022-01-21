using SM.Application.Contracts.ProductFeature.DTOs;

namespace SM.Application.Contracts.ProductFeature.Commands;

public record CreateProductFeatureCommand
    (CreateProductFeatureDto ProductFeature) : IRequest<Response<string>>;