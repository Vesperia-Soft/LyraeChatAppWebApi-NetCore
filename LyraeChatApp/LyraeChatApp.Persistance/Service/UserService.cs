using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUsers(User user)
    {
       using(var context = _unitOfWork.Create())
        {
          await  context.Repositories.userCommandRepository.AddAsync(user);
            context.SaveChanges();
        }
    }

    public User Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            //if (result == null)
            //    throw new Exception($"bu {id} değeri Id Bulunamadı");
            var result = context.Repositories.userQueryRepository.GetById(id).Result;
            
            return result;
        }
    }

    public IQueryable<User> GetAllUsers()
    {
        using(var context = _unitOfWork.Create())
        {
            var result =context.Repositories.userQueryRepository.GetAll();

            return result;
        }
    }

    public async Task RemoveUsers(int id)
    {
        using (var context = _unitOfWork.Create())
        {
           await  context.Repositories.userCommandRepository.RemoveById(id);

            context.SaveChanges();
        }
    }

    public  void UpdateUsers(User user)
    {
        using (var context = _unitOfWork.Create())
        {
            
           context.Repositories.userCommandRepository.Update(user);

            context.SaveChanges();
        }
    }
}
