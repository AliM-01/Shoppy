using SM.Application.Contracts.Product.DTOs;

namespace SM.Application.Contracts.Product.Queries;

public record ExistsProductIdQuery
    (string ProductId) : IRequest<ApiResult<ExistsProductIdResponseDto>>;