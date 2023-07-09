using LyraeChatApp.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace LyraeChatApp.Domain.Repositories.GenericRepositories.AppDbContext;

public interface IAppRepository<T> where T : EntityBase
{
   DbSet<T> EntitySet { get; set; }
}
