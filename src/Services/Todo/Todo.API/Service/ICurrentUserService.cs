namespace Todo.API.Service;

public interface ICurrentUserService
{
    string Email { get; set; }
    bool IsAuthenticated { get; set; }
}