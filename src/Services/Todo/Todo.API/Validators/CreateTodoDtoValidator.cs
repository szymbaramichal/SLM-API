using FluentValidation;
using Todo.API.Dtos.Query;

namespace Todo.API.Validators;

public class CreateTodoDtoValidator : AbstractValidator<CreateTodoDto>
{
    public CreateTodoDtoValidator()
    {
        RuleFor(model => model.EndDate).NotEmpty().GreaterThan(DateTime.Now);
        RuleFor(model => model.TodoType).NotEmpty();
        RuleFor(model => model.Title).NotEmpty();
    }
}
