using HHRU_Console.Data.Utils;
using MongoDB.Driver;

namespace HHRU_Console.Data.DAO;

internal abstract class BaseDAO
{
    private readonly MongoDBContext _mongoDBContext;
    protected readonly string _databaseName;

    protected BaseDAO(MongoDBContext mongoDBContext, string databaseName)
    {
        _mongoDBContext = mongoDBContext;
        _databaseName = databaseName;
    }

    protected IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var client = _mongoDBContext.GetConnection();
        var database = client.GetDatabase(_databaseName);
        return database.GetCollection<T>(collectionName);
    }
}
