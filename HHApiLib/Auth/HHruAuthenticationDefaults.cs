namespace AspNet.Security.OAuth.HHru;

public static class HHruAuthenticationDefaults
{
    public const string AuthenticationScheme = "hhru";
    public static readonly string DisplayName = "hhru";
    public static readonly string Issuer = "hhru";
    public static readonly string CallbackPath = "/api/Account/loginredirect";
    public static readonly string AuthorizationEndpoint = "https://hh.ru/oauth/authorize";
    public static readonly string TokenEndpoint = "https://hh.ru/oauth/token";
}
