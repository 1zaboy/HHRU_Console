using HHApiLib.Configurations;
using HHRU_Console.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HHRU_Console.Core.Configuration;

public static class HHruConsoleServiceCollectionExtensions
{
    public static IServiceCollection AddHHruConsoleCoreServices(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        HHruConfigurations.DateTimeSerializer();

        services.AddTransient<IResponseService, ResponseService>();
        services.AddTransient<IAccountService, AccountService>();

        return services;
    }

}
