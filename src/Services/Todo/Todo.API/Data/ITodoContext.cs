using MongoDB.Driver;
using Todo.API.Entities;

namespace Todo.API.Data;

public interface ITodoContext
{
    IMongoCollection<TodoEntity> Todos { get; }
}
