using HHRU_Console.Data.DAO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HHRU_Console.Data.Utils;

public class MongoDBContext
{
    private readonly IServiceProvider m_ServiceProvider;
    private readonly string _connectionString;

    public MongoDBContext(IServiceProvider serviceProvider, IOptions<MongoDBConnectionOptions> options)
    {
        m_ServiceProvider = serviceProvider;
        _connectionString = options.Value.ConnectionString;
    }

    public T Get<T>() where T : IDataAccessObject 
    {
        return m_ServiceProvider.GetRequiredService<T>();
    }

    public MongoClient GetConnection()
    {
        return new MongoClient(_connectionString);
    }
}
