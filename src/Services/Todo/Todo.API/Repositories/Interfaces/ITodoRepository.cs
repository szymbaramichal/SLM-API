using System.Linq.Expressions;
using Todo.API.Entities;

namespace Todo.API.Repositories.Interfaces;

public interface ITodoRepository
{
    Task<ICollection<TodoEntity>> GetTodos(Expression<Func<TodoEntity, bool>> exp);

    Task<TodoEntity> GetTodo(string id);

    Task<TodoEntity>CreateTodo(TodoEntity todoEntity);

    Task<bool> UpdateTodo(TodoEntity todoEntity);

    Task<bool> DeleteTodo(string id);
}
