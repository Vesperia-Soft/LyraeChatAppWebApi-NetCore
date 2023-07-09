using LyraeChatApp.Domain.Common;
using LyraeChatApp.Domain.UnitOfWork;
using Microsoft.Extensions.Configuration;
namespace LyraeChatApp.Persistance.UnitOfWorkSql;

public class UnitOfWorkSqlServer : IUnitOfWork
{
   private readonly IConfiguration _configuration;

    public UnitOfWorkSqlServer(IConfiguration configuration = null)
    {
        _configuration = configuration;
    }

    public IUnitOfWorkAdapter Create()
    {
        var connectionString = Parameters.ConnectionString;

        return new UnitOfWorkSqlServerAdapter(connectionString);
    }
}
