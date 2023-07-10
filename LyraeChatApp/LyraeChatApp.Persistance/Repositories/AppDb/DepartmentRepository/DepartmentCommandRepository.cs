using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.DepartmentRepository;

public class DepartmentCommandRepository : Repository, IDepartmentCommandRepository
{
    public DepartmentCommandRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }
    public async Task AddAsync(Department model)
    {
        var query = "INSERT INTO Department " +
            "(Name,CreatedDate,CreatorName,DeletedDate," +
            "DeleterName,UpdateDate,UpdaterName,IsActive) " +
            "VALUES " +
            "(@name, @createddate,@creatorname," +
            "@deletedDate,@deletername,@updatedate,@updatername,@isactive); " +
            "SELECT SCOPE_IDENTITY();";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@createddate", model.CreatedDate);
        command.Parameters.AddWithValue("@creatorname", model.CreatorName);
        command.Parameters.AddWithValue("@deletedDate", model.DeletedTime == null ? DBNull.Value : model.DeletedTime);
        command.Parameters.AddWithValue("@deletername", model.DeleterName == null ? DBNull.Value : model.DeleterName);
        command.Parameters.AddWithValue("@updatedate", model.UpdateDate == null ? DBNull.Value : model.UpdateDate);
        command.Parameters.AddWithValue("@updatername", model.UpdaterName == null ? DBNull.Value : model.UpdaterName);
        command.Parameters.AddWithValue("@isactive", model.IsActive);
        await command.ExecuteNonQueryAsync();
    }

    public Task AddRangeAsync(IEnumerable<Department> model)
    {
        throw new NotImplementedException();
    }

    public void Remove(Department entity)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveById(int id)
    {
        var command = CreateCommand("delete from Department where Id=@id");
        command.Parameters.AddWithValue("@id", id);
        await command.ExecuteNonQueryAsync();
    }

    public void RemoveRange(IEnumerable<Department> model)
    {
        throw new NotImplementedException();
    }

    public void Update(Department entity)
    {
        var query = "update Department set Name=@name where Id=@id";
        var command = CreateCommand(query);
        command.Parameters.AddWithValue("@name", entity.Name);
        command.Parameters.AddWithValue("@id", entity.Id);

        command.ExecuteNonQuery();
    }

    public void UpdateRange(IEnumerable<Department> model)
    {
        throw new NotImplementedException();
    }
}
