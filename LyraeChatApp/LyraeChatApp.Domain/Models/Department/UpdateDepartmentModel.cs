namespace LyraeChatApp.Domain.Models.Department;

public sealed class UpdateDepartmentModel
{
    public int Id{ get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string UpdaterName { get; set; }
}
