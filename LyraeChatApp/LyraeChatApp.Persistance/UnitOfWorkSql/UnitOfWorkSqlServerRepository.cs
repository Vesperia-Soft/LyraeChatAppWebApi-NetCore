using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.RoomRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.Repositories.AppDb.DepartmentRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.RoomRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;
using System.Data.SqlClient;

namespace LyraeChatApp.Persistance.UnitOfWorkSql;

public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
{
    public IUserCommandRepository userCommandRepository { get; }

    public IUserQueryRepository userQueryRepository { get; }

    public IDepartmentCommandRepository departmentCommandRepository { get; }

    public IDepartmentQueryRepository departmentQueryRepository { get; }

    public IRoomCommandRepository roomCommandRepository { get; }

    public IRoomQueryRepository roomQueryRepository { get; }
  

    public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
    {
        userCommandRepository = new UserCommandRepository(context, transaction);
        userQueryRepository =  new UserQueryRepository(context, transaction);
        departmentCommandRepository = new DepartmentCommandRepository(context,transaction);
        departmentQueryRepository = new DepartmentQueryRepository(context, transaction);
        roomCommandRepository = new RoomCommandRepository(context, transaction);
        roomQueryRepository = new RoomQueryRepository(context, transaction);
    }
}
