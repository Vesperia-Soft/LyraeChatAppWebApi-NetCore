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
        var connectionString = _configuration == null
            ? Parameters.ConnectionString
            : _configuration.GetConnectionString("SqlConnectionString");

        return new UnitOfWorkSqlServerAdapter("Data Source=localhost;Initial Catalog=example;Integrated Security=True");
    }
}
