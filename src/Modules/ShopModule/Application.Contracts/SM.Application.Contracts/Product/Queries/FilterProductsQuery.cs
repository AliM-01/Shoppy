using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public record FilterProductsQuery
    (FilterProductDto Filter) : IRequest<Response<FilterProductDto>>;