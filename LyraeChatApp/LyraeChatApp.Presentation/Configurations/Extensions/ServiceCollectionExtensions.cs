using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.UnitOfWorkSql;

namespace LyraeChatApp.Presentation.Configurations.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ApplicationServiceConfigurations(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkSqlServer>();

        return services;
    }
}
