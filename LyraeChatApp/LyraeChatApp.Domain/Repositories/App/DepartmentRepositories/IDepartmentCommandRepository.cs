using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;

public interface IDepartmentCommandRepository
{
    Task AddAsync(Department model);
    Task AddRangeAsync(IEnumerable<Department> model);
    void Update(Department entity);
    void UpdateRange(IEnumerable<Department> model);
    Task RemoveById(int id);
    void Remove(Department entity);
    void RemoveRange(IEnumerable<Department> model);
}
