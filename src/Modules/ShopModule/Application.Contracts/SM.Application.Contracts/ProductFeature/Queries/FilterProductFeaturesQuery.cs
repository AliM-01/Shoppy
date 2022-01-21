using SM.Application.Contracts.ProductFeature.DTOs;

namespace SM.Application.Contracts.ProductFeature.Queries;

public record FilterProductFeaturesQuery
    (FilterProductFeatureDto Filter) : IRequest<Response<FilterProductFeatureDto>>;