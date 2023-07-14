using LyraeChatApp.Application.Services.Utilities;

namespace LyraeChatApp.Persistance.Service.Utilities;

public class FileService : IFileService
{
    public string GetMimeType(string fileName)
    {
        string ext = Path.GetExtension(fileName);

        switch (ext.ToLower())
        {
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".gif":
                return "image/gif";
            default:
                return "application/octet-stream";
        }
    }
}
