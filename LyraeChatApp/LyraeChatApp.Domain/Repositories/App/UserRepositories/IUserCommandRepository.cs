using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.Repositories.GenericRepositories.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyraeChatApp.Domain.Repositories.App.UserRepositories
{
    internal interface IUserCommandRepository :IAppCommandRepository<User>
    {
    }
}
