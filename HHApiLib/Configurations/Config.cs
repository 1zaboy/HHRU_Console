namespace HHApiLib.Configurations;

public record Config(string MainUrl, string MainApiUrl, string ClientId, string ClientSecret)
{
    internal string AuthorizationRedirect => @$"{MainUrl}/oauth/authorize?response_type=code&client_id={ClientId}";
}
