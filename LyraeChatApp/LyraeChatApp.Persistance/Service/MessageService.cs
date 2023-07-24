using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Message;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class MessageService : IMessageService
{
    #region Fields
    private IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogService _logService;
    #endregion
    #region Ctor
    public MessageService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogService logService
     )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logService = logService;
    }
    #endregion

    #region Methods

    public async Task Create(CreateMessageModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var entity = _mapper.Map<Message>(model);
                await context.Repositories.messageCommandRepository.AddAsync(entity);
                context.SaveChanges();

                _logService.LogToDb("Mesaj oluşturuldu", model.CreatorName);
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Mesaj oluşturulurken hata oluştu: {ex.Message}", model.CreatorName);
                throw;
            }
        }
    }
    public async Task<MessageModel> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var condition = context.Repositories.messageQueryRepository.CheckMessagetId(id).Result;
                if (!condition)
                    throw new Exception("Listelemek istediğiniz mesaj sistemde bulunamadı");
                var result = context.Repositories.messageQueryRepository.GetById(id).Result;
                return result;
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Mesaj listelenirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }
    public PaginationHelper<Message> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                var result = context.Repositories.messageQueryRepository.GetAll(request.PageNumber, request.PageSize);
                return result;
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Mesaj listelenirken hata oluştu: {ex.Message}", "");
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
                if (!await context.Repositories.messageQueryRepository.CheckMessagetId(id))
                    throw new Exception("Silmek istediğiniz mesaj sistemde bulunamadı");

                await context.Repositories.messageCommandRepository.RemoveById(id);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Mesaj silinirken hata oluştu: {ex.Message}", id.ToString());
                throw;
            }
        }
    }
    public void Update(UpdateMessageModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            try
            {
                if (!context.Repositories.messageQueryRepository.CheckMessagetId(model.Id).Result)
                    throw new Exception("Güncellemek istediğiniz mesaj sistemde bulunamadı");

                var entity = _mapper.Map<Message>(model);
                entity.UpdateDate = DateTime.Now;
                context.Repositories.messageCommandRepository.Update(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logService.LogToDb($"Mesaj güncellenirken hata oluştu: {ex.Message}", model.UpdaterName);
                throw;
            }
        }
    }
    #endregion
}
