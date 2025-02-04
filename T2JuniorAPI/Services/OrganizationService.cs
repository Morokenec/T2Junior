using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Services;

public class OrganizationService : IOrganizationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrganizationService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrganizationDto>> GetAllOrganizationsAsync()
    {
        var organizations = await _context.Organizations
            .Where(o => o.IsDelete == false)
            .ToListAsync();
        return _mapper.Map<List<OrganizationDto>>(organizations);
    }

    public async Task<string> CreateOrganization(OrganizationDto organization)
    {
        if (await _context.Organizations.AnyAsync(o => o.Name == organization.Name))
        {
            return "Organization alredy exist";
        }

        var newOrganzation = _mapper.Map<Organization>(organization);

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

    public async Task<string> UpdateOrganization(Guid id, OrganizationDto organizationDto)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
            return "Organization not found";

        _mapper.Map(organizationDto, organization);
        organization.UpdateDate = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        { 
            return $"An error occurred while updating the organization: {ex.Message}";
        }

        return "success";
    }

    public async Task<string> DeleteOrganization(Guid id)
    {
        var organization = await _context.Organizations.FindAsync(id);
        if (organization == null)
        {
            return "Organization not found";
        }

        organization.IsDelete = true;
        organization.UpdateDate = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return $"An error occurred while deleting the organization: {ex.Message}";
        }

        return "Success";
    }
}
