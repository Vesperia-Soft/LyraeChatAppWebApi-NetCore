using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.Mapping;
using LyraeChatApp.Persistance.Service;
using LyraeChatApp.Persistance.UnitOfWorkSql;

namespace LyraeChatApp.Presentation.Configurations.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationServiceConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ILogService, LogService>();
        services.AddEndpointsApiExplorer();
        services.AddControllers();

        return services;
    }
}
