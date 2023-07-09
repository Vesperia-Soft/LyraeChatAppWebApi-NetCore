using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Models;

public class User :EntityBase
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string CellPhone { get; set; }
    public string Email { get; set; }

}
