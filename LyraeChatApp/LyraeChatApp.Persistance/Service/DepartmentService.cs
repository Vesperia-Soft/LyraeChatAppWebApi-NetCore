using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public sealed class DepartmentService : IDepartmentService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public DepartmentService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreateDepartmentModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<Department>(model);
            entity.CreatedDate = DateTime.Now;
            await context.Repositories.departmentCommandRepository.AddAsync(entity);
            context.SaveChanges();
        }
    }

    public async Task<Department> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(id).Result)
                throw new Exception("Listelemek istediğiniz departman sistemde bulunamadı");
            var result = context.Repositories.departmentQueryRepository.GetById(id).Result;
            return result;
        }
    }

    public PaginationHelper<Department> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.departmentQueryRepository.GetAll(request.PageNumber, request.PageSize);
            return result;
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(id).Result)
                throw new Exception("Silmek istediğiniz departman sistemde bulunamadı");
            await context.Repositories.departmentCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public async void Update(UpdateDepartmentModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(model.Id).Result)
                throw new Exception("Güncellemek istediğiniz departman sistemde bulunamadı");

            var entity = _mapper.Map<Department>(model);
            entity.UpdateDate = DateTime.Now;
            context.Repositories.departmentCommandRepository.Update(entity);
            context.SaveChanges();
        }
    }
}
