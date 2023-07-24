using LyraeChatApp.Domain.ResponseDtosModel;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.ControllerBased;

public class CustomBaseController  :ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responseDto)
    {
        return new ObjectResult(responseDto)
        {
            StatusCode= Response.StatusCode
        };
    }
}
