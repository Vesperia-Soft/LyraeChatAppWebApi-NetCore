namespace LyraeChatApp.Domain.Models.Room;

public class CreateRoomModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public IList<int> UserId { get; set; }
}
