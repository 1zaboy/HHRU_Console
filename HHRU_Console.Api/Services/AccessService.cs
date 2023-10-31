using HHApiLib.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace HHRU_Console.Api.Services;

public class AccessService : IAccessService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccessService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        return await _httpContextAccessor.HttpContext?.GetTokenAsync("access_token") ?? throw new NullReferenceException();
    }

    public Task<string> GetEmailAsync()
    {
        var identity = _httpContextAccessor.HttpContext?.User?.Identities.FirstOrDefault(x => x.HasClaim(c => c.Type == ClaimsIdentity.DefaultNameClaimType));
        var claim = identity?.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;

        if (!string.IsNullOrEmpty(claim))
        {
            return Task.FromResult(claim);
        }

        return null;
    }
}
