using HHApiLib.Configurations;
using HHRU_Console.Core.Quartz;
using HHRU_Console.Core.Services;
using HHRU_Console.Core.Services.Host;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;

namespace HHRU_Console.Core.Configuration;

public static class HHruConsoleServiceCollectionExtensions
{
    public static IServiceCollection AddHHruConsoleCoreServices(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        HHruConfigurations.DateTimeSerializer();

        services.AddSingleton<IResponseService, ResponseService>();
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IResumeService, ResumeService>();
        services.AddSingleton<ResumeAdvancingService>();

        services.AddSingleton<IJobFactory, SingletonJobFactory>();
        services.AddSingleton<ResumeAdvancingJob>();

        services.AddHostedService<ResumeUpdateInit>();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }

}
