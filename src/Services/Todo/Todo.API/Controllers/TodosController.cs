using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]
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

        var todoDto = mapper.Map<TodoDto>(todoEntity);

        return Ok(todoDto);
    }

    [HttpGet("{id:length(24)}")]
    [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<TodoDto>> GetTodoById(string id)
    {
        var todoEntity = await repository.GetTodo(id);

        if(todoEntity is null) return NotFound(ResourceString.NotFoundById);

        var todoDto = mapper.Map<TodoDto>(todoEntity);

        return Ok(todoDto);
    }

    [HttpGet]
    [ProducesResponseType(typeof(TodoDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ICollection<TodoDto>>> ListTodosForTimestamp([FromQuery] ListTodosForTimestampDto listTodosForTimestampDto)
    {
        var todoEntity = await repository.GetTodosForTimestamp(listTodosForTimestampDto.SinceDate, listTodosForTimestampDto.DueDate);

        if (!todoEntity.Any()) return NotFound(ResourceString.NotFoundByDate);

        var todoDto = mapper.Map<List<TodoDto>>(todoEntity);

        return Ok(todoDto);
    }

    [HttpPut]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateTodo(TodoDto todoDto)
    {
        var todoEntity = await repository.GetTodo(todoDto.Id);

        if(todoEntity is null) return NotFound(ResourceString.NotFoundById);

        var isUpdatedCorrectly = await repository.UpdateTodo(mapper.Map(todoDto, todoEntity));

        if (isUpdatedCorrectly) return Ok();

        return BadRequest(ResourceString.BadRequest);
    }

    [HttpDelete]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteTodo(TodoDto todoDto)
    {
        var isDeletedCorrectly = await repository.DeleteTodo(todoDto.Id);

        if (!isDeletedCorrectly) return NotFound(ResourceString.NotFoundById);

        return Ok();
    }
}
