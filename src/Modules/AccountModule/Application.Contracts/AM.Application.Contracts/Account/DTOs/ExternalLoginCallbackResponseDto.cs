using AM.Application.Contracts.Account.Enums;

namespace AM.Application.Contracts.Account.DTOs;
public class ExternalLoginCallbackResponseDto
{
    public ExternalLoginCallbackResponseDto()
    {

    }

    public ExternalLoginCallbackResponseDto(ExternalLoginCallbackResult type)
    {
        Type = type;
    }

    public ExternalLoginCallbackResult Type { get; set; }

    public string Email { get; set; }

    public AuthenticateUserResponseDto? TokenResult { get; set; }
}
