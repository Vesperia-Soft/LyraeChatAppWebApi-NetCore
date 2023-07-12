namespace LyraeChatApp.Domain.Models.User;

public class UserLoginResponseModel
{
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string PasswordHash { get; set; }
}
