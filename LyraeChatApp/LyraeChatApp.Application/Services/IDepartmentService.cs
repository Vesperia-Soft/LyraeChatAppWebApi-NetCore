using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.ResponseDtosModel;

namespace LyraeChatApp.Application.Services;

public interface IDepartmentService
{
    Task<ResponseDto<Department>> Get(int id);
    Task<ResponseDto<PaginationHelper<Department>>> GetAll(PaginationRequest request);
    Task Create(CreateDepartmentModel model);
    void Update(UpdateDepartmentModel model);
    Task Remove(int id);
}
