using HHRU_Console.Data.DAO;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace HHRU_Console.Data;

public static class MongoDBDataServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDBDataContext(this IServiceCollection services, Action<MongoDBConnectionOptions> options)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSingleton<MongoDBContext>();
        services.AddSingleton<IResumeUpdateDAO, ResumeUpdateDAO>();

        services.Configure(options);

        return services;
    }
}
