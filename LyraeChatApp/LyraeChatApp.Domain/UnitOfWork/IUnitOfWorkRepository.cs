using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.LogRepositories;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;

namespace LyraeChatApp.Domain.UnitOfWork;

public interface IUnitOfWorkRepository
{
    #region UserRepositories
    IUserCommandRepository userCommandRepository { get; }
    IUserQueryRepository userQueryRepository { get; }
    #endregion

    #region DepartmentRepositories
    IDepartmentCommandRepository departmentCommandRepository { get; }
    IDepartmentQueryRepository departmentQueryRepository { get; }
    #endregion

    #region MessageRepositories
    IMessageCommandRepository messageCommandRepository { get; }
    IMessageQueryRepository messageQueryRepository { get; }
    #endregion

    #region LogRepositories
    ILogCommandRepository logCommandRepository { get; }
    #endregion
}
