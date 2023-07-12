using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.Message;

namespace LyraeChatApp.Domain.Repositories.App.MessageRepositories;

public interface IMessageQueryRepository
{
    PaginationHelper<Message> GetAll(int pageNumber, int pageSize);
    IQueryable<Message> GetWhere();
    Task<MessageModel> GetById(int Id);
    Task<Message> GetFirstByExpression();
    Task<Message> GetFirst();
    Task<bool> CheckMessagetId(int id);
}
