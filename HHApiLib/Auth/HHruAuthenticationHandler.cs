using Flurl;
using Flurl.Http;
using HHApiLib.Auth;
using HHApiLib.Configurations;
using HHApiLib.Services;
using HHApiLib.Utils;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AspNet.Security.OAuth.HHru;

public partial class HHruAuthenticationHandler : OAuthHandler<HHruAuthenticationOptions>, IAuthenticationSignOutHandler
{
    private readonly IAccessService _tokenService;

    public HHruAuthenticationHandler(
        [NotNull] IOptionsMonitor<HHruAuthenticationOptions> options,
        [NotNull] ILoggerFactory logger,
        [NotNull] UrlEncoder encoder,
        [NotNull] ISystemClock clock,
        [NotNull] IAccessService tokenService)
        : base(options, logger, encoder, clock)
    {
        _tokenService = tokenService;
    }

    protected override async Task<AuthenticationTicket> CreateTicketAsync([NotNull] ClaimsIdentity identity, [NotNull] AuthenticationProperties properties, [NotNull] OAuthTokenResponse tokens)
    {
        try
        {
            var response = await $"{Options.UserInformationEndpoint}"
                .WithOAuthBearerToken(tokens.AccessToken)
                .WithHeader("HH-User-Agent", $"kvd_hh_control/1.0 (kvd_z1cat@gmail.com)")
                .GetStringAsync();

            using var container = JsonDocument.Parse(response);
            var payload = container.RootElement;

            var email = payload.GetString("email");
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, email));

            //properties.RedirectUri = "https://localhost:4200";

            var principal = new ClaimsPrincipal(identity);
            var context = new OAuthCreatingTicketContext(principal, properties, Context, Scheme, Options, Backchannel, tokens, payload);
            context.RunClaimActions();

            // Re-run to get the email claim from the tokens response
            context.RunClaimActions(tokens.Response!.RootElement);

            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal!, context.Properties, Scheme.Name);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        if (string.IsNullOrEmpty(properties.RedirectUri))
        {
            properties.RedirectUri = OriginalPathBase + OriginalPath + Request.QueryString;
        }

        // OAuth2 10.12 CSRF
        GenerateCorrelationId(properties);

        var authorizationEndpoint = BuildChallengeUrl(properties, BuildRedirectUri(Options.CallbackPath));
        var redirectContext = new RedirectContext<OAuthOptions>(
            Context, Scheme, Options,
            properties, authorizationEndpoint);

        var location = Context.Response.Headers.Location;
        if (location == StringValues.Empty)
        {
            location = "(not set)";
        }

        var cookie = Context.Response.Headers.SetCookie;
        if (cookie == StringValues.Empty)
        {
            cookie = "(not set)";
        }

        Response.StatusCode = 401;
        await Response.WriteAsJsonAsync<AuthReturn>(new()
        {
            ClientId = Setup.Conf.ClientId,
            State = Options.StateDataFormat.Protect(properties)
        });
    }

    protected override async Task<OAuthTokenResponse> ExchangeCodeAsync(Microsoft.AspNetCore.Authentication.OAuth.OAuthCodeExchangeContext context)
    {
        try
        {
            var data = await $"{Options.TokenEndpoint}"
                .SetQueryParam("client_id", Options.ClientId)
                .SetQueryParam("client_secret", Options.ClientSecret)
                .SetQueryParam("code", context.Code)
                .SetQueryParam("grant_type", "authorization_code")
                .PostAsync();

            var body = await data.GetStringAsync();
            return OAuthTokenResponse.Success(JsonDocument.Parse(body));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
    {
        var parameters = new Dictionary<string, string>
            {
                { "client_id", Options.ClientId },
                { "response_type", "code" },
                { "redirect_uri", "https://localhost:4200" }
            };

        parameters["state"] = Options.StateDataFormat.Protect(properties);

        return QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, parameters!);
    }

    private async Task RemoveToken()
    {
        var accessToken = await _tokenService.GetAccessTokenAsync();
        await $"{Options.TokenEndpoint}"
        .WithOAuthBearerToken(accessToken)
        .WithHeaderAgent()
        .DeleteAsync();
    }

    public async Task SignOutAsync(AuthenticationProperties? properties)
    {
        await RemoveToken();
        await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
