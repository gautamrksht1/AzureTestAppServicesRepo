﻿namespace EcommerceWebClient.Services.Interfaces
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
