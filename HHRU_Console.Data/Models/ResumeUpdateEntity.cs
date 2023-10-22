using MongoDB.Bson.Serialization.Attributes;

namespace HHRU_Console.Data.Models;

public class ResumeUpdateEntity
{
    [BsonId]
    public string Id { get; set; }
    public bool IsAdcanving { get; set; }
    public DateTime? AdcanvingAt { get; set; }

}
