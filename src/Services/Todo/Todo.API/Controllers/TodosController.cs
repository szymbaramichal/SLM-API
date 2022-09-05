using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Todo.API.Dtos;
using Todo.API.Dtos.Query;
using Todo.API.Entities;
using Todo.API.Repositories.Interfaces;

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
    public async Task<ActionResult<TodoDto>> CreateProduct(CreateTodoDto createTodoDto)
    {
        await repository.CreateTodo(mapper.Map<TodoEntity>(createTodoDto));

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }
}
