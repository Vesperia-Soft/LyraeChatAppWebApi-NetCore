using LyraeChatApp.Domain.Core;
using System.Linq.Expressions;

namespace LyraeChatApp.Domain.Repositories.GenericRepositories.AppDbContext;

public  interface IAppQueryRepository<T> where T : EntityBase
{
    IQueryable<T> GetAll();
    IQueryable<T> GetWhere();
    Task<T> GetById(string id);
    Task<T> GetFirstByExpression();
    Task<T> GetFirst();
}
