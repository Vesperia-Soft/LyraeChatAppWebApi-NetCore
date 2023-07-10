using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;

namespace LyraeChatApp.Application.Services;

public interface IDepartmentService
{
    Task<Department> Get(int id);
    PaginationHelper<Department> GetAll(PaginationRequest request);
    Task Create(CreateDepartmentModel model);
    void Update(UpdateDepartmentModel model);
    Task Remove(int id);
}
