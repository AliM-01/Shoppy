using SM.Application.Contracts.ProductFeature.DTOs;

namespace SM.Application.Contracts.ProductFeature.Queries;

public record GetProductFeatureDetailsQuery
    (long Id) : IRequest<Response<EditProductFeatureDto>>;