using AutoMapper;
using Todo.API.Dtos;
using Todo.API.Dtos.Query;
using Todo.API.Entities;

namespace Todo.API.Mappings;

public class MainProfile : Profile
{
    public MainProfile()
    {
        CreateMap<TodoEntity, TodoDto>();
        CreateMap<CreateTodoDto, TodoEntity>();
    }
}
