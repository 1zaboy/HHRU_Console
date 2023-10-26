using AspNet.Security.OAuth.HHru;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class HHruAuthenticationExtensions
{
    public static AuthenticationBuilder AddHHru(this AuthenticationBuilder builder)
    {
        return builder.AddHHru(HHruAuthenticationDefaults.AuthenticationScheme, options => { });
    }

    public static AuthenticationBuilder AddHHru(this AuthenticationBuilder builder, Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddHHru(HHruAuthenticationDefaults.AuthenticationScheme, configuration);
    }

    public static AuthenticationBuilder AddHHru(this AuthenticationBuilder builder, string scheme, Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddHHru(scheme, HHruAuthenticationDefaults.DisplayName, configuration);
    }

    public static AuthenticationBuilder AddHHru(this AuthenticationBuilder builder, string scheme, string caption, Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddOAuth<HHruAuthenticationOptions, HHruAuthenticationHandler>(scheme, caption, configuration);
    }
}
