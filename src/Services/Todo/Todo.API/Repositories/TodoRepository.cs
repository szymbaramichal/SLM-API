using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using Todo.API.Data;
using Todo.API.Entities;
using Todo.API.Repositories.Interfaces;

namespace Todo.API.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ITodoContext context;

    public TodoRepository(ITodoContext context)
    {
        this.context = context;
    }

    public async Task<string> CreateTodo(TodoEntity todoEntity)
    {
        if (todoEntity is null)
        {
            throw new ArgumentNullException(nameof(todoEntity));
        }

        await context.Todos.InsertOneAsync(todoEntity);

        return todoEntity.Id;
    }

    public async Task<bool> DeleteTodo(string id)
    {
        FilterDefinition<TodoEntity> filter = Builders<TodoEntity>.Filter.Eq(x => x.Id, id);
        var result = await context.Todos.DeleteOneAsync(filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<TodoEntity> GetTodo(string id)
    {
        return await context.Todos.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ICollection<TodoEntity>> GetTodos()
    {
        return await context.Todos.Find(x => true).ToListAsync();
    }

    public async Task<ICollection<TodoEntity>> GetTodos(Expression<Func<TodoEntity, bool>> exp)
    {
        IMongoQueryable<TodoEntity> results = context.Todos.AsQueryable().Where(exp);
        return await results.ToListAsync();
    }

    public async Task<bool> UpdateTodo(TodoEntity todoEntity)
    {
        var result = await context.Todos.ReplaceOneAsync(filter: p => p.Id == todoEntity.Id, replacement: todoEntity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }
}
