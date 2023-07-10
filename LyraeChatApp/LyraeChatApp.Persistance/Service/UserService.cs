using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Helpers;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Domain.UnitOfWork;

namespace LyraeChatApp.Persistance.Service;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task CreateUsers(CreateUserModel user)
    {
        using (var context = _unitOfWork.Create())
        {
            var userEntity = _mapper.Map<User>(user);
            await context.Repositories.userCommandRepository.AddAsync(userEntity);
            context.SaveChanges();
        }
    }

    public async Task< User> Get(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            var checkUserId = await  context.Repositories.userQueryRepository.CheckUserId(id);

          if (checkUserId == false)
              throw new Exception($"bu {id} değeri Id Bulunamadı");
            var result = context.Repositories.userQueryRepository.GetById(id).Result;

            return result;
        }
    }

    public PaginationHelper<UserListModel> GetAllUsers(PaginationRequest request)
    {
        using (var context = _unitOfWork.Create())
        {
            var result = context.Repositories.userQueryRepository.GetAll(request.PageNumber, request.PageSize);

            return result;
        }
    }

    public async Task RemoveUsers(int id)
    {
        using (var context = _unitOfWork.Create())
        {
            await context.Repositories.userCommandRepository.RemoveById(id);

            context.SaveChanges();
        }
    }

    public void UpdateUsers(User user)
    {
        using (var context = _unitOfWork.Create())
        {

            context.Repositories.userCommandRepository.Update(user);

            context.SaveChanges();
        }
    }
}
