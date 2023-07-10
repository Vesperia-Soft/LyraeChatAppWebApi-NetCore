using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Models.User;

public class User:EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }

}
