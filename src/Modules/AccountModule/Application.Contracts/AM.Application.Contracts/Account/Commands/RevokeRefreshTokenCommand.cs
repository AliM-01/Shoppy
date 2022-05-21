using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Commands;

public record RevokeRefreshTokenCommand(RevokeRefreshTokenRequestDto Token) : IRequest<AuthenticateUserResponseDto>;
