using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Todo.API.Enums;

public enum TodoType
{
    [BsonRepresentation(BsonType.Int32)]
    Task = 1,
    [BsonRepresentation(BsonType.Int32)]
    Important = 2 
}
