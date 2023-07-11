namespace LyraeChatApp.Domain.Models.Room;

public  class UpdateRoomModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}
