using Flurl.Http;
using HHApiLib.Configurations;
using HHApiLib.Models;
using HHApiLib.Utils;

namespace HHApiLib.Apis;

public class ActiveClientApi : ApiBase
{
    public ActiveClientApi(string token) : base(token)
    {
    }

    public async Task<ActiveClient> GetClientAsync()
    {
        return await $"{Setup.Conf.MainApiUrl}/me"
            .WithOAuthBearerToken(Token)
            .WithHeaderAgent()
            .GetJsonAsync<ActiveClient>();
    }
}
