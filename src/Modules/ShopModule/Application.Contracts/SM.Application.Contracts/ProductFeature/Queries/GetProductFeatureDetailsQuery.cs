using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Queries;

public record GetProductFeatureDetailsQuery(string Id) : IRequest<EditProductFeatureDto>;