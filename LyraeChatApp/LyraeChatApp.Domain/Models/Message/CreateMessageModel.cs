namespace LyraeChatApp.Domain.Models.Message;

public class CreateMessageModel
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public int RoomId { get; set; }
    public string CreatorName { get; set; }
}
