using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.Message;

namespace LyraeChatApp.Application.Services;

public interface IMessageService
{
    Task<MessageModel> Get(int id);
    PaginationHelper<Message> GetAll(PaginationRequest request);
    Task Create(CreateMessageModel model);
    void Update(UpdateMessageModel model);
    Task Remove(int id);
}
