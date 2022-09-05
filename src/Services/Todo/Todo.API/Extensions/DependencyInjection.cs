using Todo.API.Data;
using Todo.API.Repositories;
using Todo.API.Repositories.Interfaces;

namespace Todo.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterCustomDI(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITodoContext, TodoContext>();
        services.AddScoped<ITodoRepository, TodoRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
