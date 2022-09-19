using System.Linq.Expressions;
using Todo.API.Entities;

namespace Todo.API.Repositories.Interfaces;

public interface ITodoRepository
{
    /// <summary>
    /// Get todos which match given predicate
    /// </summary>
    /// <param name="exp">Expression</param>
    /// <returns>Collection of todos</returns>
    Task<ICollection<TodoEntity>> GetTodos(Expression<Func<TodoEntity, bool>> exp);

    /// <summary>
    /// Get todoEntity by id
    /// </summary>
    /// <param name="id">Todo id</param>
    /// <returns>TodoEntity</returns>
    Task<TodoEntity> GetTodo(string id);

    /// <summary>
    /// Create todoEntity
    /// </summary>
    /// <param name="todoEntity">TodoEntity</param>
    /// <returns>Entity with id</returns>
    Task<TodoEntity>CreateTodo(TodoEntity todoEntity);

    /// <summary>
    /// Update todoEntity
    /// </summary>
    /// <param name="todoEntity">TodoEntity to be updated</param>
    /// <returns>True -> if correctly updated</returns>
    Task<bool> UpdateTodo(TodoEntity todoEntity);

    /// <summary>
    /// Delete todoEntity
    /// </summary>
    /// <param name="id">Id of todoEntity</param>
    /// <returns>True -> if correctly deleted</returns>
    Task<bool> DeleteTodo(string id);
}
