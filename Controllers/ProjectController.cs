using Microsoft.AspNetCore.Mvc;
using WelderProjectManagement.Dtos;
using WelderProjectManagement.Services;

namespace WelderProjectManagement.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private readonly ProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WebResponse<string>), StatusCodes.Status201Created)]
    public async Task<ActionResult<WebResponse<string>>> AddProject([FromBody] ProjectAddRequest request)
    {
        await projectService.AddProject(request);
        return WebResponse<string>.Success("OK", HttpContext);
    }
}
