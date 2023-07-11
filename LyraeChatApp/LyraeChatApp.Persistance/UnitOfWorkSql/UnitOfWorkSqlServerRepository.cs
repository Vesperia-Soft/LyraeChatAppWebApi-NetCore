using LyraeChatApp.Domain.Repositories.App.DepartmentRepositories;
using LyraeChatApp.Domain.Repositories.App.MessageRepositories;
using LyraeChatApp.Domain.Repositories.App.UserRepositories;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Persistance.Repositories.AppDb.DepartmentRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.MessageRepository;
using LyraeChatApp.Persistance.Repositories.AppDb.UserRepository;
using System.Data.SqlClient;

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
    #endregion

    #region CTOR
    public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
    {
        userCommandRepository = new UserCommandRepository(context, transaction);
        userQueryRepository =  new UserQueryRepository(context, transaction);
        departmentCommandRepository = new DepartmentCommandRepository(context,transaction);
        departmentQueryRepository = new DepartmentQueryRepository(context, transaction);
        messageCommandRepository = new MessageCommandRepository(context, transaction);
        messageQueryRepository = new MessageQueryRepository(context, transaction);
    }
    #endregion
}
