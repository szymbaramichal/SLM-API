using AutoMapper;
using Todo.API.Dtos;
using Todo.API.Dtos.Query;
using Todo.API.Entities;

namespace Todo.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoEntity, TodoDto>();
        CreateMap<CreateTodoDto, TodoEntity>().ForMember(src => src.Id, opt => opt.Ignore())
            .ForMember(src => src.IsCompleted, opt => opt.Ignore())
            .ForMember(src => src.UserId, opt => opt.Ignore())
            .ForMember(src => src.CreationDate, opt => opt.Ignore())
            .ForMember(src => src.ModificationDate, opt => opt.Ignore());
    }
}
