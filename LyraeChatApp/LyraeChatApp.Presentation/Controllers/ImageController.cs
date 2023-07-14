using LyraeChatApp.Application.Services.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{

    #region Fields
    private readonly IFileService _fileService;
    #endregion

    #region Ctor
    public ImageController(IFileService fileService)
    {
        _fileService = fileService;
    }
    #endregion

    #region Methods
    [HttpGet("[action]/{fileName}")]
    public IActionResult GetImage(string fileName)
    {
        string path = "./Content/Images/" + fileName;
        byte[] fileBytes = System.IO.File.ReadAllBytes(path);

        string mimeType = _fileService.GetMimeType(fileName);

        return File(fileBytes, mimeType);
    }
    #endregion

}
