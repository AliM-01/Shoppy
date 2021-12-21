using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public record GetProductDetailsQuery(long Id) : IRequest<Response<EditProductDto>>;