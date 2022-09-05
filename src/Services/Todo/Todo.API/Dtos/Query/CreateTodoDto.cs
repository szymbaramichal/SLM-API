using Todo.API.Enums;

namespace Todo.API.Dtos.Query;

public class CreateTodoDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EndDate { get; set; }
    public TodoType TodoType { get; set; }
}
