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

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WebResponse<WebPagination<List<ProjectResponse>>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<WebResponse<WebPagination<List<ProjectResponse>>>>> GetProjects([FromQuery] int page = 1, [FromQuery] int size = 10)
    {
        var response = await projectService.GetProjects(page, size);
        return WebResponse<WebPagination<List<ProjectResponse>>>.Success(response, HttpContext);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WebResponse<ProjectResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<WebResponse<ProjectResponse>>> GetProjectById([FromRoute] long id)
    {
        var response = await projectService.GetProjectById(id);
        return WebResponse<ProjectResponse>.Success(response, HttpContext);
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WebResponse<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult<WebResponse<string>>> UpdateProject([FromRoute] long id, [FromBody] ProjectUpdateRequest request)
    {
        await projectService.UpdateProject(id, request);
        return WebResponse<string>.Success("OK", HttpContext);
    }
}
