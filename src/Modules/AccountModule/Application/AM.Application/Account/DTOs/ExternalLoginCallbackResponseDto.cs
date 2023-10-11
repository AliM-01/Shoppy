using AM.Application.Account.Enums;

namespace AM.Application.Account.DTOs;

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