using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HHApiLib.Configurations;

public static class HHruConfigurations
{
    public static void DateTimeSerializer()
    {
        FlurlHttp.Configure(settings =>
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
            };
            settings.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
        });
    }
}
