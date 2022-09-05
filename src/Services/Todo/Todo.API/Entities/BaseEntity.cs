using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Todo.API.Entities;
public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime ModificationDate { get; set; }
}

