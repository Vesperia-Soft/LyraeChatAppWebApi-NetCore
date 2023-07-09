namespace LyraeChatApp.Domain.UnitOfWork;

public interface IUnitOfWork
{
    IUnitOfWorkAdapter Create();
}
