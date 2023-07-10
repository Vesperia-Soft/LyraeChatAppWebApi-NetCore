using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;

public interface IDepartmentQueryRepository
{
    PaginationHelper<Department> GetAll(int pageNumber, int pageSize);
    IQueryable<Department> GetWhere();
    Task<Department> GetById(int Id);
    Task<Department> GetFirstByExpression();
    Task<Department> GetFirst();
    Task<bool> CheckDepartmentId(int id);
}
