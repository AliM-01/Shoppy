using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public record ExistsProductIdQuery
    (long ProductId) : IRequest<Response<ExistsProductIdResponseDto>>;