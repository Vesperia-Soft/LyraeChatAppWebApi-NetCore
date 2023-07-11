using LyraeChatApp.Domain.Core;

namespace LyraeChatApp.Domain.Models.Message;

public class Message : EntityBase
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime TimeStamps { get; set; }
}
