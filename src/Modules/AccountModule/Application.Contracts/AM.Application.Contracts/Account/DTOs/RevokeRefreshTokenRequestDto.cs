﻿namespace AM.Application.Contracts.Account.DTOs;

public class RevokeRefreshTokenRequestDto
{
    public RevokeRefreshTokenRequestDto(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
    [JsonProperty("refreshToken")]
    public string RefreshToken { get; set; }
}
