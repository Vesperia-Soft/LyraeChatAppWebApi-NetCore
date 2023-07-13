using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services.Utilities;

public interface IJwtService
{
    string CreateToken(User user);
}
