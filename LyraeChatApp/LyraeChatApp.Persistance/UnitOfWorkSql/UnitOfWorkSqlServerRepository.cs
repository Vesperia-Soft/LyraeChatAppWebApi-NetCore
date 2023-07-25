using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using LyraeChatApp.Domain.Repositories.App.LogRepositories;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.Repositories.AppDb.DepartmentRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.RoomRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.LogRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.MessageRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;
using System.Data.SqlClient;
using LyraeChatApp.Domain.Repositories.App.UserRoom;
using LyraeChatApp.Persistance.Repositories.AppDb.UserRoom;

namespace LyraeChatApp.Persistance.UnitOfWorkSql;

public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
{
    #region Fields
    public IUserCommandRepository userCommandRepository { get; }
    public IUserQueryRepository userQueryRepository { get; }
    public IDepartmentCommandRepository departmentCommandRepository { get; }
    public IDepartmentQueryRepository departmentQueryRepository { get; }
    public IMessageCommandRepository messageCommandRepository { get; }
    public IMessageQueryRepository messageQueryRepository { get; }
    public ILogCommandRepository logCommandRepository { get; }

    public IRoomCommandRepository roomCommandRepository { get; }

    public IRoomQueryRepository roomQueryRepository { get; }

    public IUserRoomCommandRepository userRoomCommandRepository { get; }

    public IUserRoomQueryRepository userRoomQueryRepository { get; }
    #endregion


    #region CTOR
    public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
    {
        userCommandRepository = new UserCommandRepository(context, transaction);
        userQueryRepository = new UserQueryRepository(context, transaction);
        departmentCommandRepository = new DepartmentCommandRepository(context, transaction);
        departmentQueryRepository = new DepartmentQueryRepository(context, transaction);
        roomCommandRepository = new RoomCommandRepository(context, transaction);
        roomQueryRepository = new RoomQueryRepository(context, transaction);
        messageCommandRepository = new MessageCommandRepository(context, transaction);
        messageQueryRepository = new MessageQueryRepository(context, transaction);
        logCommandRepository = new LogCommandRepository(context, transaction);
        userRoomCommandRepository= new UserRoomCommandRepository(context,transaction);
        userRoomQueryRepository= new UserRoomQueryRepository(context, transaction);
    }
    #endregion
}
