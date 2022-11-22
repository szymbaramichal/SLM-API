using MongoDB.Driver;
using MongoDB.Driver.Linq;
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

    public async Task<TodoEntity> CreateTodo(TodoEntity todoEntity)
    {
        NullCheck(todoEntity);

        await context.Todos.InsertOneAsync(todoEntity);

        return todoEntity;
    }

    public async Task<bool> DeleteTodo(string id)
    {
        var filter = Builders<TodoEntity>.Filter.Eq(todo => todo.Id, id);
        var result = await context.Todos.DeleteOneAsync(filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<TodoEntity> GetTodo(string id)
    {
        return await context.Todos.Find(todo => todo.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ICollection<TodoEntity>> GetTodosForTimestamp(DateTime sinceDate, DateTime dueDate)
    {
        IMongoQueryable<TodoEntity> results = context.Todos.AsQueryable().Where(todo => todo.CreationDate >= sinceDate && todo.EndDate <= dueDate);
        return await results.ToListAsync();
    }

    public async Task<bool> UpdateTodo(TodoEntity todoEntity) 
    {
        NullCheck(todoEntity);

        var result = await context.Todos.ReplaceOneAsync(filter: todo => todo.Id == todoEntity.Id, replacement: todoEntity);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    private static void NullCheck(object obj)
    {
        if (obj is null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
    }
}
