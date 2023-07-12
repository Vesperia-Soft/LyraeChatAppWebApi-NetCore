using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.Message;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.MessageRepository;

public class MessageQueryRepository : Repository, IMessageQueryRepository
{
    public MessageQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task<bool> CheckMessagetId(int id)
    { 
        var command = CreateCommand("SELECT COUNT(*) FROM Messages WHERE Id = @id");

        command.Parameters.AddWithValue("@id", id);

        var result = await command.ExecuteScalarAsync();

        int count;
        if (int.TryParse(result?.ToString(), out count))
        {
            return count > 0;
        }

        return false;
    }

    public PaginationHelper<Message> GetAll(int pageNumber, int pageSize)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Messages");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM Messages ORDER BY Id OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<Message> messages = new List<Message>();
            while (reader.Read())
            {
                messages.Add(new Message
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Text = reader["Text"].ToString(),
                    UserId = Convert.ToInt32(reader["UserId"]),
                    RoomId = Convert.ToInt32(reader["RoomId"]),
                    TimeStamps = Convert.ToDateTime(reader["TimeStamps"])
                });
            }

            return new PaginationHelper<Message>(totalCount, pageSize, pageNumber, messages);
        }
    }

    public async Task<MessageModel> GetById(int Id)
    {
        var command = CreateCommand("SELECT * FROM Messages WHERE Id =@id");

        command.Parameters.AddWithValue("@id", Id);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new MessageModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                Text = reader["Text"].ToString(),
                UserId = Convert.ToInt32(reader["UserId"]),
                RoomId = Convert.ToInt32(reader["RoomId"]),
                TimeStamps = Convert.ToDateTime(reader["TimeStamps"])
            };
        }
    }

    public Task<Message> GetFirst()
    {
        throw new NotImplementedException();
    }

    public Task<Message> GetFirstByExpression()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Message> GetWhere()
    {
        throw new NotImplementedException();
    }
}
