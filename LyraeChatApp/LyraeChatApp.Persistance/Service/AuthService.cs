using AutoMapper;
using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.User;
using LyraeChatApp.Domain.UnitOfWork;
using System.Linq;

namespace LyraeChatApp.Persistance.Service;

public class AuthService : IAuthService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ChangePassword(ChangePasswordModel model)
    {
        using (var context = _unitOfWork.Create())
        {
            var user = await context.Repositories.userQueryRepository.GetByMail(model.Email);
            user.PasswordHash = model.NewPassword;
            context.Repositories.userCommandRepository.UpdatePass(user);
            context.SaveChanges();
        }
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
        var checkImageUserModel = CheckToImageFile(userModel);
        using (var context = _unitOfWork.Create())
        {
            var userEntity = _mapper.Map<User>(checkImageUserModel);
            await context.Repositories.userCommandRepository.AddAsync(userEntity);
            context.SaveChanges();
        }
    }

    private CreateUserModel CheckToImageFile(CreateUserModel model)
    {
        var fileName = model.Image.FileName;
        var ext = fileName.Substring(fileName.LastIndexOf('.'));
        var extensions = ext.ToLower();
        List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
        var imgSize = model.Image.Length;
        decimal decimalMbimgSize = Convert.ToDecimal(imgSize * 0.000001);

        if (!AllowFileExtensions.Contains(extensions))
        {
            throw new Exception("Eklediğiniz resim tipi gerçerli değil");
        }
        if (decimalMbimgSize > 1)
        {
            throw new Exception("Eklediğiniz resim boyutu en fazla 1mb olabilir.");
        }

        return model;
    }


}
