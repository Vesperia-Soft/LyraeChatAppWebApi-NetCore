namespace LyraeChatApp.Domain.Models.LogModels;

public class LogModel
{
    public string UserName { get; set; }
    public string LogMessage { get; set; }
    public DateTime LogDate { get; set; } = DateTime.Now;
}
