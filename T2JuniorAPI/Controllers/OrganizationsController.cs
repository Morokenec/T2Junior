using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Organizations;
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
    public async Task<ActionResult<List<OrganizationDto>>> GetAllOrganizations()
    {
        return await _organizationService.GetAllOrganizationsAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto organization)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _organizationService.CreateOrganization(organization);
        return Ok(result);
    }

    // PUT: api/Organizations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, [FromBody] OrganizationDto organizationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _organizationService.UpdateOrganization(id, organizationDto);
        return Ok(result);
    }

    // DELETE: api/Organizations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var result = await _organizationService.DeleteOrganization(id);
        return Ok(result);
    }
}
