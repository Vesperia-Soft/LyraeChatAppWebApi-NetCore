namespace LyraeChatApp.Domain.Models.User;

public class UserListModel
{
    public virtual int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Name { get; set; }
    public string SurName { get; set; }
    public string Photo { get; set; }
    public int DepartmanId { get; set; }
    public virtual bool? IsActive { get; set; }
}