using MongoDB.Driver;
using Todo.API.Entities;
using Todo.API.Enums;

namespace Todo.API.Data;

public static class TodoContextSeed
{
    public static void SeedData(IMongoCollection<TodoEntity> todosCollection)
    {
        bool existProduct = todosCollection.Find(p => true).Any();
        if (!existProduct)
        {
            todosCollection.InsertManyAsync(GetTodosData());
        }
    }

    private static IEnumerable<TodoEntity> GetTodosData()
    {
        return new List<TodoEntity>()
            {
                new TodoEntity()
                {
                    Id = "602d2149e773f2a3990b47f8",
                    Description = "",
                    Title = "",
                    EndDate = DateTime.MaxValue,
                    TodoType = TodoType.Important,
                    UserId = "",
                    IsCompleted = false,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
                new TodoEntity()
                {
                    Id = "602d2149e773f2a3990b47f9",
                    Description = "",
                    Title = "",
                    EndDate = DateTime.MaxValue,
                    TodoType = TodoType.Task,
                    UserId = "",
                    IsCompleted = false,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
                new TodoEntity()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Description = "",
                    Title = "",
                    EndDate = DateTime.MaxValue,
                    TodoType = TodoType.Task,
                    UserId = "",
                    IsCompleted = false,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
                new TodoEntity()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Description = "",
                    Title = "",
                    EndDate = DateTime.MaxValue,
                    TodoType = TodoType.Important,
                    UserId = "",
                    IsCompleted = false,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
                new TodoEntity()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Description = "",
                    Title = "",
                    EndDate = DateTime.MaxValue,
                    TodoType = TodoType.Important,
                    UserId = "",
                    IsCompleted = false,
                    CreationDate = DateTime.UtcNow,
                    ModificationDate = DateTime.UtcNow
                },
            };
    }
}
