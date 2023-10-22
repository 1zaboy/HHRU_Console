using HHApiLib.Services;
using Microsoft.AspNetCore.Authentication;

namespace HHRU_Console.Api.Services;

public class TokenService : ITokenService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        return await _httpContextAccessor.HttpContext.GetTokenAsync("access_token") ?? throw new NullReferenceException();
    }
}
