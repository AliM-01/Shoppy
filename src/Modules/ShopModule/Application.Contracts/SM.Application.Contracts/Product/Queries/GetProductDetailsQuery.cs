using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public record GetProductDetailsQuery(string Id) : IRequest<ApiResult<EditProductDto>>;