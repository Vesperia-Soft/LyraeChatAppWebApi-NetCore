using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Repositories.GenericRepositories.AppDbContext;

public  interface IAppCommandRepository<T> where T : EntityBase
{
    Task AddAsync(T model);
    Task AddRangeAsync(IEnumerable<T> model);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> model);
    Task RemoveById(string id);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> model);
}
