using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using LyraeChatApp.Presentation.ControllerBased;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : CustomBaseController
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }



    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll([FromQuery] PaginationRequest request)
    {
        var users = await _departmentService.GetAll(request);

        return CreateActionResultInstance(users);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _departmentService.Get(id);

        return CreateActionResultInstance(user);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create(CreateDepartmentModel model)
    {
        await _departmentService.Create(model);
        return Ok(model);
    }

    [HttpPut("[action]")]
    public IActionResult Update(UpdateDepartmentModel department)
    {
        _departmentService.Update(department);
        return Ok();
    }

    [HttpDelete("(id)")]
    public IActionResult Remove(int id)
    {
        _departmentService.Remove(id);
        return Ok();
    }
}
