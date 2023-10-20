using AspNet.Security.OAuth.HHru;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection;

public static class HHruAuthenticationExtensions
{
    public static AuthenticationBuilder AddHHru([NotNull] this AuthenticationBuilder builder)
    {
        return builder.AddHHru(HHruAuthenticationDefaults.AuthenticationScheme, options => { });
    }

    public static AuthenticationBuilder AddHHru([NotNull] this AuthenticationBuilder builder, [NotNull] Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddHHru(HHruAuthenticationDefaults.AuthenticationScheme, configuration);
    }

    public static AuthenticationBuilder AddHHru([NotNull] this AuthenticationBuilder builder, [NotNull] string scheme, [NotNull] Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddHHru(scheme, HHruAuthenticationDefaults.DisplayName, configuration);
    }

    public static AuthenticationBuilder AddHHru([NotNull] this AuthenticationBuilder builder, [NotNull] string scheme, [CanBeNull] string caption, [NotNull] Action<HHruAuthenticationOptions> configuration)
    {
        return builder.AddOAuth<HHruAuthenticationOptions, HHruAuthenticationHandler>(scheme, caption, configuration);
    }
}
