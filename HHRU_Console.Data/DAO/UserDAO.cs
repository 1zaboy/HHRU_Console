using HHRU_Console.Data.Models;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HHRU_Console.Data.DAO;

internal class UserDAO : BaseDAO, IUserDAO
{
    private const string DB_COLLECTION = "USER";

    public UserDAO(MongoDBContext mongoDBContext, IOptions<MongoDBConnectionOptions> options) : base(mongoDBContext, options.Value.DatabaseName)
    {
    }

    public async Task<UserEntity> GetAsync(string id)
    {
        var collection = GetCollection<UserEntity>(DB_COLLECTION);
        var filter = Builders<UserEntity>.Filter.Eq("_id", id);
        var data = await collection.FindAsync(filter);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<string> SetAsync(UserEntity entity)
    {
        var collection = GetCollection<UserEntity>(DB_COLLECTION);
        var filter = Builders<UserEntity>.Filter.Eq("_id", entity.Email);
        await collection.ReplaceOneAsync(filter, entity, new ReplaceOptions() { IsUpsert = true });
        return entity.Email;
    }
}
