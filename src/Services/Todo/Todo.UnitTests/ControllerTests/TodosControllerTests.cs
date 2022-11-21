using Bogus;
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

        var todoEntity = _mapper.Map<TodoEntity>(createTodoDto);

        _repoMock.Setup(x => x.CreateTodo(It.IsAny<TodoEntity>())).Returns(Task.FromResult(todoEntity));
        _mapperMock.Setup(x => x.Map<TodoDto>(It.IsAny<TodoEntity>())).Returns(_mapper.Map<TodoDto>(todoEntity));

        var controller = new TodosController(_repoMock.Object, _mapperMock.Object);
        var result = await controller.CreateTodo(createTodoDto);

        result.Value.ShouldNotBeNull();
        result.Value.Description.ShouldBe(createTodoDto.Description);
    }
}