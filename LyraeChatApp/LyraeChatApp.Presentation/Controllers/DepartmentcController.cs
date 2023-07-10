using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.Models.Department;
using LyraeChatApp.Domain.Models.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace LyraeChatApp.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet("[action]")]
    public IActionResult GetAll([FromQuery] PaginationRequest request)
    {
        var users = _departmentService.GetAll(request);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<string>> Get(int id)
    {
        return Ok(
            _departmentService.Get(id)
        );
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
