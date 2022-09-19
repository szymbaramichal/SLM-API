namespace Todo.API.Dtos.Query;

public class ListTodosForTimestampDto
{
    public DateTime SinceDate { get; set; }
    public DateTime DueDate { get; set; }
}
