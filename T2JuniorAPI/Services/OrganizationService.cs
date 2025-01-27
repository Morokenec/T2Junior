using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Services;

public class OrganizationService : IOrganizationService
{
    private readonly ApplicationDbContext _context;

    public OrganizationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrganizationDto>> GetAllOrganizationsAsync()
    {
        return await _context.Organizations
            .Select(o => new OrganizationDto
            {
                Id = o.Id,
                Name = o.Name
            })
            .ToListAsync();
    }
}
