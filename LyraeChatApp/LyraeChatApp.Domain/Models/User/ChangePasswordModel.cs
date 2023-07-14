namespace LyraeChatApp.Domain.Models.User;

public class ChangePasswordModel
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}
