using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Domain.ResponseDtosModel;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public sealed class DepartmentService : IDepartmentService
{
    #region Fields
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private readonly ILogService _logService;
    #endregion
    #region Ctor
    public DepartmentService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogService logService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logService = logService;
    }
    #endregion

    #region Methods
    public async Task Create(CreateDepartmentModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var entity = _mapper.Map<Department>(model);
                entity.CreatedDate = DateTime.Now;
                await context.Repositories.departmentCommandRepository.AddAsync(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Departman oluşturulurken hata oluştu: {ex.Message}", model.CreatorName);
                throw;
            }
        }
    }

    public async Task<ResponseDto<DepartmentModel>> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(id).Result)
                    throw new Exception("Listelemek istediğiniz departman sistemde bulunamadı");
                var result = context.Repositories.departmentQueryRepository.GetById(id).Result;

                var department = _mapper.Map<DepartmentModel>(result);

                return ResponseDto<DepartmentModel>.Success(department, 200);
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Departman listelenirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }
    public async Task<ResponseDto<PaginationHelper<DepartmentListModel>>> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var result = context.Repositories.departmentQueryRepository.GetAll(request.PageNumber, request.PageSize);


                var paginationHelper = new PaginationHelper<DepartmentListModel>(
                    result.TotalCount, request.PageSize, request.PageNumber, null);

                var departmentList = result.Items.Select(item => _mapper.Map<DepartmentListModel>(item)).ToList();

                paginationHelper.Items = departmentList;

                return ResponseDto<PaginationHelper<DepartmentListModel>>.Success(paginationHelper, 200);
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Departman listelenirken hata oluştu: {ex.Message}", "");
                throw;
            }
        }
    }
    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(id).Result)
                    throw new Exception("Silmek istediğiniz departman sistemde bulunamadı");
                await context.Repositories.departmentCommandRepository.RemoveById(id);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Departman silinirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }

    public async void Update(UpdateDepartmentModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.departmentQueryRepository.CheckDepartmentId(model.Id).Result)
                    throw new Exception("Güncellemek istediğiniz departman sistemde bulunamadı");

                var entity = _mapper.Map<Department>(model);
                entity.UpdateDate = DateTime.Now;
                context.Repositories.departmentCommandRepository.Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Departman güncellenirken hata oluştu: {ex.Message}", model.UpdaterName);
                throw;
            }
        }
    }
    #endregion
}

