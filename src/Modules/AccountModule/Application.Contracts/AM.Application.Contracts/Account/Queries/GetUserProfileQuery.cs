using AM.Application.Contracts.Account.DTOs;

namespace AM.Application.Contracts.Account.Queries;

public record GetUserProfileQuery(string UserId) : IRequest<UserProfileDto>;
