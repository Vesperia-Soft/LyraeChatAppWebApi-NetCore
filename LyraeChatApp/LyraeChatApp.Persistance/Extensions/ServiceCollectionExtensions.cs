using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.UnitOfWorkSql;
using Microsoft.Extensions.DependencyInjection;

namespace LyraeChatApp.Persistance.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection PersistanceServiceConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkSqlServer>();
        return services;
    }
}
