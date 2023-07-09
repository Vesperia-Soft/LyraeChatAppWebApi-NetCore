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
}
