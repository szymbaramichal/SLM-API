using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Todo.API.Dtos;
using Todo.API.Dtos.Query;
using Todo.API.Entities;
using Todo.API.Repositories.Interfaces;
using Todo.API.Resources;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoRepository repository;
    private readonly IMapper mapper;

    public TodosController(ITodoRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<TodoDto>> CreateTodo(CreateTodoDto createTodoDto)
    {
        var todoEntity = await repository.CreateTodo(mapper.Map<TodoEntity>(createTodoDto));

        return Ok(mapper.Map<TodoDto>(todoEntity));
    }

    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<TodoDto>> GetTodoById(string id)
    {
        var todoEntity = await repository.GetTodo(id);

        if(todoEntity is null) return NotFound(ResourceString.NotFoundById);

        return mapper.Map<TodoDto>(todoEntity);
    }

    [HttpGet]
    [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ICollection<TodoDto>>> ListTodosForTimestamp([FromQuery] ListTodosForTimestampDto getTodoSinceDateDto)
    {
        var todoEntity = await repository.GetTodos(x => x.EndDate >= getTodoSinceDateDto.SinceDate && x.EndDate <= getTodoSinceDateDto.DueDate);

        if (!todoEntity.Any()) return NotFound(ResourceString.NotFoundByDate);

        return mapper.Map<List<TodoDto>>(todoEntity);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTodo(TodoDto todoDto)
    {
        var todoEntity = await repository.GetTodo(todoDto.Id);

        var isUpdatedCorrectly = await repository.UpdateTodo(mapper.Map(todoDto, todoEntity));

        if(!isUpdatedCorrectly) return NotFound(ResourceString.NotFoundById);

        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteTodo(TodoDto todoDto)
    {
        var updatedEntity = await repository.DeleteTodo(todoDto.Id);

        return updatedEntity;
    }
}
