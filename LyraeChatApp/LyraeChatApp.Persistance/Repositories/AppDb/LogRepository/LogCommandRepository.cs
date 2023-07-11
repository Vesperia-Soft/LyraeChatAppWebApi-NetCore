using LyraeChatApp.Domain.Models.LogModels;
using LyraeChatApp.Domain.Repositories.App.LogRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.LogRepository;

public class LogCommandRepository : Repository, ILogCommandRepository
{
    public LogCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task AddAsync(LogModel model)
    {
        var query = "INSERT INTO LogTable " +
            "(UserName,LogMessage,LogDate) " +
            "VALUES " +
            "(@username, @logmessage,@logdate); " +
            "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@username", model.UserName);
        command.Parameters.AddWithValue("@logmessage", model.LogMessage);
        command.Parameters.AddWithValue("@logdate", model.LogDate);
        await command.ExecuteNonQueryAsync();
    }
}
