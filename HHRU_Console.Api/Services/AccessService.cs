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

    // TODO: Rework func
    public Task<string> GetEmailAsync()
    {
        var identity = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault(x => x.FindFirst(ClaimsIdentity.DefaultNameClaimType) != null);
        var claim = identity.FindFirst(ClaimsIdentity.DefaultNameClaimType);
        return Task.FromResult(claim.Value);
    }
}
