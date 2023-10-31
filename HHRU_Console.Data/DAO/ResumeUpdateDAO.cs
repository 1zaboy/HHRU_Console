using HHRU_Console.Data.Models;
using HHRU_Console.Data.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HHRU_Console.Data.DAO;

internal class ResumeUpdateDAO : IResumeUpdateDAO
{
    private const string DB_COLLECTION = "RESUME_UPDATE";
    private readonly string _databaseName;
    private readonly MongoDBContext _mongoDBContext;

    public ResumeUpdateDAO(MongoDBContext mongoDBContext, IOptions<MongoDBConnectionOptions> options)
    {
        _mongoDBContext = mongoDBContext;
        _databaseName = options.Value.DatabaseName;
    }

    public async Task<ResumeUpdateEntity> GetAsync(string id)
    {
        var collection = GetCollection();
        var filter = Builders<ResumeUpdateEntity>.Filter.Eq("_id", id);
        var data = await collection.FindAsync(filter);
        return await data.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ResumeUpdateEntity>> GetAllAsync()
    {
        var collection = GetCollection();
        return await collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<string> SetAsync(ResumeUpdateEntity model)
    {
        var collection = GetCollection();
        await collection.InsertOneAsync(model);
        return model.Id;
    }

    public async Task<bool> UpdateAsync(string id, ResumeUpdateEntity entity)
    {
        var collection = GetCollection();
        var filter = Builders<ResumeUpdateEntity>.Filter.Eq(r => r.Id, id);

        var update = Builders<ResumeUpdateEntity>.Update
            .Set(x => x.IsAdcanving, entity.IsAdcanving)
            .Set(x => x.OwnerEmail, entity.OwnerEmail);

        if (entity.AdcanvingAt.HasValue)
            update = update.Set(x => x.AdcanvingAt, entity.AdcanvingAt.Value);

        var updateResult = await collection.UpdateOneAsync(filter, update);
        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var collection = GetCollection();
        var filter = Builders<ResumeUpdateEntity>.Filter.Eq(r => r.Id, id);

        var updateResult = await collection.DeleteOneAsync(filter);
        return updateResult.DeletedCount > 0;
    }

    public async Task<bool> CheckAsync(string id)
    {
        var data = await GetAsync(id);
        return data != null;
    }

    private IMongoCollection<ResumeUpdateEntity> GetCollection()
    {
        var client = _mongoDBContext.GetConnection();
        var database = client.GetDatabase(_databaseName);
        return database.GetCollection<ResumeUpdateEntity>(DB_COLLECTION);
    }
}
