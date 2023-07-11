namespace LyraeChatApp.Domain.Models.Message;

public class MessageModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public DateTime TimeStamps { get; set; }
}
