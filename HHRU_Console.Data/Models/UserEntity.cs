using MongoDB.Bson.Serialization.Attributes;

namespace HHRU_Console.Data.Models;

public class UserEntity
{
    [BsonId]
    public string Email { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string ExpiresIn { get; set; }
}
