using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.LogRepositories;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRoom;

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

    #region Room 
    IRoomCommandRepository roomCommandRepository { get; }
    IRoomQueryRepository roomQueryRepository { get; }
    #endregion

    #region UserRoom
    IUserRoomCommandRepository userRoomCommandRepository { get; }
    IUserRoomQueryRepository userRoomQueryRepository { get; }
    #endregion

}
