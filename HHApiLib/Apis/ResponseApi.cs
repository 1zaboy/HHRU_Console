using Flurl.Http;
using HHApiLib.Configurations;
using HHApiLib.Models;
using HHApiLib.Models.Response;
using HHApiLib.Utils;

namespace HHApiLib.Apis;

public class ResponseApi : ApiBase
{
    public ResponseApi(string token) : base(token)
    {
    }

    public async Task<ArrayWrapper<Response>> GetResponsesAsync(int perPage)
    {
        return await $"{Setup.Conf.MainApiUrl}/negotiations"
            .WithOAuthBearerToken(Token)
            .WithHeaderAgent()
            .SetQueryParam("per_page", perPage)
            .GetJsonAsync<ArrayWrapper<Response>>();
    }
}
