using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;

namespace LyraeChatApp.Domain.UnitOfWork;

public interface IUnitOfWorkRepository
{
    IUserCommandRepository userCommandRepository { get; }
    IUserQueryRepository userQueryRepository { get; }

    IDepartmentCommandRepository departmentCommandRepository { get; }
    IDepartmentQueryRepository departmentQueryRepository { get; }

    IRoomCommandRepository roomCommandRepository { get; }
    IRoomQueryRepository roomQueryRepository { get; }
}
