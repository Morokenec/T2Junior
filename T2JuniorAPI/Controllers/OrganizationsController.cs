using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations()
    {
        var organizations = await _organizationService.GetAllOrganizationsAsync();
        return Ok(organizations);
    }
}
