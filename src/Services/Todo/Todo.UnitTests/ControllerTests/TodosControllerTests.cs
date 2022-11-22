using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Todo.API.Controllers;
using Todo.API.Dtos;
using Todo.API.Dtos.Query;
using Todo.API.Entities;
using Todo.API.Enums;
using Todo.UnitTests.Common;
using Todo.UnitTests.Mapping;

namespace Todo.UnitTests.ControllerTests;

public class TodosControllerTests : UnitTestBase
{
    public TodosControllerTests(MappingTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Post_CreateTodo_Success()
    {
        var createTodoDto = new Faker<CreateTodoDto>()
            .RuleFor(src => src.EndDate, faker => faker.Date.Future())
            .RuleFor(src => src.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(src => src.Title, faker => faker.Random.String())
            .RuleFor(src => src.TodoType, faker => faker.PickRandom<TodoType>()).Generate();

        var createTodoAsEntity = _mapper.Map<TodoEntity>(createTodoDto);

        _repoMock.Setup(x => x.CreateTodo(It.IsAny<TodoEntity>())).ReturnsAsync(createTodoAsEntity);
        _mapperMock.Setup(x => x.Map<TodoDto>(createTodoAsEntity)).Returns(_mapper.Map<TodoDto>(createTodoAsEntity));

        var controller = new TodosController(_repoMock.Object, _mapperMock.Object);

        var result = await controller.CreateTodo(createTodoDto);
        result.ShouldBeOfType<ActionResult<TodoDto>>();
        result.Result.ShouldNotBeNull();

        var okObjectResult = result.Result as ObjectResult;
        var resultDto = okObjectResult.Value as TodoDto;


        resultDto.ShouldBeEquivalentTo(_mapper.Map<TodoDto>(createTodoAsEntity));
    }

    [Fact]
    public async Task Get_GetTodoById_Success()
    {
        var todoId = new Faker().Random.Number(100).ToString();
        var todoEntity = new Faker<TodoEntity>().Generate();
        var todoDto = _mapper.Map<TodoDto>(todoEntity);

        _repoMock.Setup(x => x.GetTodo(It.IsAny<string>())).ReturnsAsync(todoEntity);
        _mapperMock.Setup(x => x.Map<TodoDto>(todoEntity)).Returns(todoDto);

        var controller = new TodosController(_repoMock.Object, _mapperMock.Object);

        var result = await controller.GetTodoById(todoId);
        result.ShouldBeOfType<ActionResult<TodoDto>>();
        result.Result.ShouldNotBeNull();

        var okObjectResult = result.Result as ObjectResult;
        var resultDto = okObjectResult.Value as TodoDto;


        resultDto.ShouldBeEquivalentTo(todoDto);
    }

    [Fact]
    public async Task Get_ListTodosForTimestamp_Success()
    {
        var query = new Faker<ListTodosForTimestampDto>().Generate();
        var todoEntities = new Faker<TodoEntity>().Generate(10);
        var todosDto = _mapper.Map<List<TodoDto>>(todoEntities);

        _repoMock.Setup(x => x.GetTodosForTimestamp(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(todoEntities);
        _mapperMock.Setup(x => x.Map<List<TodoDto>>(todoEntities)).Returns(todosDto);

        var controller = new TodosController(_repoMock.Object, _mapperMock.Object);

        var result = await controller.ListTodosForTimestamp(query);
        result.ShouldBeOfType<ActionResult<ICollection<TodoDto>>>();
        result.Result.ShouldNotBeNull();

        var okObjectResult = result.Result as ObjectResult;
        var resultDto = okObjectResult.Value as ICollection<TodoDto>;


        resultDto.ShouldBeEquivalentTo(todosDto);
    }

    [Fact]
    public async Task Put_UpdateTodo_Success()
    {
        //todo
    }

    [Fact]
    public async Task Delete_DeleteTodo_Success()
    {
        //todo
    }
}