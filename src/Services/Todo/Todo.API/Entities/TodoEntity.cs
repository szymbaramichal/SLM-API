using Todo.API.Enums;

namespace Todo.API.Entities;

public sealed class TodoEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime EndDate { get; set; }
    public TodoType TodoType { get; set; }
    public string UserId { get; set; } = string.Empty;
}
