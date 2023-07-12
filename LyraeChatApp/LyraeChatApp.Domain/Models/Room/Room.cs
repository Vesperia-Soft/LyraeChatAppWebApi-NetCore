using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Models.Room;

public  class Room :EntityBase
{
    public int Id { get; set; }
    public string Name { get; set; }
}
