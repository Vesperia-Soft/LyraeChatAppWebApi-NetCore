using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.Repositories.AppDb.DepartmentRepository;

public class DepartmentQueryRepository : Repository, IDepartmentQueryRepository
{
    public DepartmentQueryRepository(SqlConnection context, SqlTransaction transaction)
    {
        this._context = context;
        this._transaction = transaction;
    }

    public async Task<bool> CheckDepartmentId(int id)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Department WHERE Id = @id");

        command.Parameters.AddWithValue("@id", id);

        var result = await command.ExecuteScalarAsync();

        int count;
        if (int.TryParse(result?.ToString(), out count))
        {
            return count > 0;
        }

        return false;
    }

    public PaginationHelper<Department> GetAll(int pageNumber, int pageSize)
    {
        var command = CreateCommand("SELECT COUNT(*) FROM Department");
        int totalCount = (int)command.ExecuteScalar();

        command.CommandText = $"SELECT * FROM Department ORDER BY Id OFFSET {((pageNumber - 1) * pageSize)} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        using (var reader = command.ExecuteReader())
        {
            List<Department> users = new List<Department>();
            while (reader.Read())
            {
                users.Add(new Department
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString()
                });
            }

            return new PaginationHelper<Department>(totalCount, pageSize, pageNumber, users);
        }
    }

    public async Task<Department> GetById(int Id)
    {
        var command = CreateCommand("SELECT * FROM Department WHERE Id =@id");

        command.Parameters.AddWithValue("@id", Id);

        using (var reader = command.ExecuteReader())
        {
            reader.Read();

            return new Department
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString()
            };
        }
    }

    public Task<Department> GetFirst()
    {
        throw new NotImplementedException();
    }

    public Task<Department> GetFirstByExpression()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Department> GetWhere()
    {
        throw new NotImplementedException();
    }
}
