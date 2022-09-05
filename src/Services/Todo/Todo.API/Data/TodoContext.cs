using MongoDB.Driver;
using Todo.API.Entities;

namespace Todo.API.Data;

public class TodoContext : ITodoContext
{
    public TodoContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Todos = database.GetCollection<TodoEntity>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
        TodoContextSeed.SeedData(Todos);
    }

    public IMongoCollection<TodoEntity> Todos { get; }
}
