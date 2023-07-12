namespace LyraeChatApp.Application.Services;

public interface ILogService
{
    void LogToDb(string message, string userName);
}
