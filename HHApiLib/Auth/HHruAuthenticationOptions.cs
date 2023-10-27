using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;

namespace AspNet.Security.OAuth.HHru;

public class HHruAuthenticationOptions : OAuthOptions
{
    public string? SignOutScheme { get; set; }

    public HHruAuthenticationOptions()
    {
        CallbackPath = HHruAuthenticationDefaults.CallbackPath;

        AuthorizationEndpoint = HHruAuthenticationDefaults.AuthorizationEndpoint;
        TokenEndpoint = HHruAuthenticationDefaults.TokenEndpoint;
        UserInformationEndpoint = HHruAuthenticationDefaults.UserInformationEndpoint;

        ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
        ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
        ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
        ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
    }
}
