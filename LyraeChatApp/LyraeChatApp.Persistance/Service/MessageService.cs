using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Message;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class MessageService : IMessageService
{

    private readonly IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public MessageService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreateMessageModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var entity = _mapper.Map<Message>(model);
            entity.CreatedDate = DateTime.Now;
            entity.TimeStamps = DateTime.Now;
            await context.Repositories.messageCommandRepository.AddAsync(entity);
            context.SaveChanges();
        }
    }

    public async Task<MessageModel> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var condition = context.Repositories.messageQueryRepository.CheckMessagetId(id).Result;
            if (!condition)
                throw new Exception("Listelemek istediğiniz mesaj sistemde bulunamadı");
            var result = context.Repositories.messageQueryRepository.GetById(id).Result;
            return result;
        }
    }

    public PaginationHelper<Message> GetAll(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.messageQueryRepository.GetAll(request.PageNumber, request.PageSize);
            return result;
        }
    }

    public async Task Remove(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!await context.Repositories.messageQueryRepository.CheckMessagetId(id))
                throw new Exception("Silmek istediğiniz mesaj sistemde bulunamadı");

            await context.Repositories.messageCommandRepository.RemoveById(id);
            context.SaveChanges();
        }
    }

    public void Update(UpdateMessageModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            if (!context.Repositories.messageQueryRepository.CheckMessagetId(model.Id).Result)
                throw new Exception("Güncellemek istediğiniz mesaj sistemde bulunamadı");

            var entity = _mapper.Map<Message>(model);
            entity.UpdateDate = DateTime.Now;
            context.Repositories.messageCommandRepository.Update(entity);
            context.SaveChanges();
        }
    }
}
