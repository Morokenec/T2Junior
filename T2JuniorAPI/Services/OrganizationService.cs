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

    public async Task<string> CreateOrganization(OrganizationDto organization)
    {
        if (await _context.Organizations.AnyAsync(o => o.Name == organization.Name))
        {
            return "Organization alredy exist";
        }

        var newOrganzation = new Organization
        {
            Id = Guid.NewGuid(),
            Name = organization.Name,
            CreationDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            IsDelete = false,
        };

        await _context.Organizations.AddAsync(newOrganzation);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return $"an error while saving organization: {ex.Message}";
        }

        return "success";

    }
}
