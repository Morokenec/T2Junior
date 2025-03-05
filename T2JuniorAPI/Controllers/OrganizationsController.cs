using Microsoft.AspNetCore.Mvc;
using T2JuniorAPI.DTOs.Organizations;
using T2JuniorAPI.Services.Organizations;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Получение списков всех организаций.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpGet]
    public async Task<ActionResult<List<OrganizationDto>>> GetAllOrganizations()
    {
        return await _organizationService.GetAllOrganizationsAsync();
    }

    /// <summary>
    /// Создание организации.
    /// </summary>
    /// <param name="organization">Организация</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto organization)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _organizationService.CreateOrganization(organization);
        return Ok(result);
    }

    /// <summary>
    /// Обновление данных организации.
    /// </summary>
    /// <param name="organizationDto">Организация</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    // PUT: api/Organizations/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, [FromBody] OrganizationDto organizationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _organizationService.UpdateOrganization(id, organizationDto);
        return Ok(result);
    }

    /// <summary>
    /// Удаление организации по ID.
    /// </summary>
    /// <param name="id">Организация</param>
    /// <returns></returns>
    /// <response code="200">Успешное выполнение</response>
    /// <response code="400">Ошибка API</response>
    // DELETE: api/Organizations/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var result = await _organizationService.DeleteOrganization(id);
        return Ok(result);
    }
}
