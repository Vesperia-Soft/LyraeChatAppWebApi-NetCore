using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public  class AuthService :IAuthService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<User> CheckByUser(string userName)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.userQueryRepository.CheckUserNameAndPassword(userName);

            return result;
        }
    }

    public async Task<bool> CheckDatabaseForUser(string userName)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = await context.Repositories.userQueryRepository.CheckDatabaseForUserName(userName);

            return result;
        }
    }

    public async Task CreateUsers(CreateUserModel userModel)
    {
        using (var context = _unitOfWork.Create())
        {
            var userEntity = _mapper.Map<User>(userModel);
            await context.Repositories.userCommandRepository.AddAsync(userEntity);
            context.SaveChanges();
        }
    }
}
