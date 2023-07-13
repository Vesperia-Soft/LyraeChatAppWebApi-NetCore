using LyraeChatApp.Domain.Models.User;

namespace LyraeChatApp.Application.Services;

public  interface IJwtService
{
  string CreateToken(User user);
}
