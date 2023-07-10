namespace LyraeChatApp.Domain.Models.Department;

public sealed class CreateDepartmentModel
{
    public string Name { get; set; }
    public string CreatorName { get; set; } = "Admin";
    public bool IsActive { get; set; } = true;
}
