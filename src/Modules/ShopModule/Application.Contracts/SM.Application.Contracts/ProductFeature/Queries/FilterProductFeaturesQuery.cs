using SM.Application.ProductFeature.DTOs;

namespace SM.Application.ProductFeature.Queries;

public record FilterProductFeaturesQuery(FilterProductFeatureDto Filter) : IRequest<FilterProductFeatureDto>;