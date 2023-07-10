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

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("LyraeChatAppConnection");

    }
    public IUnitOfWorkAdapter Create()
    {
        var connectionString = GetConnectionString();

        return new UnitOfWorkSqlServerAdapter(connectionString);
    }
}
