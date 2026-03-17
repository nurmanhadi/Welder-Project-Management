using Microsoft.AspNetCore.Mvc;
using WelderProjectManagement.Dtos;
using WelderProjectManagement.Services;

namespace WelderProjectManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService clientService;

    public ClientController(ClientService clientService)
    {
        this.clientService = clientService;
    }

    [HttpPut("{id}")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(WebResponse<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult<WebResponse<string>>> UpdateClient([FromRoute] long id, [FromBody] ClientUpdateRequest request)
    {
        await clientService.UpdateClient(id, request);
        return WebResponse<string>.Success("OK", HttpContext);
    }
}
