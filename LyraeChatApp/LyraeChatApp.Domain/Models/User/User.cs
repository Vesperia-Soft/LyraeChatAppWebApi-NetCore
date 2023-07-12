using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Models.User;

public  class User :EntityBase
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Photo { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public int DepartmanId { get; set; }
}
