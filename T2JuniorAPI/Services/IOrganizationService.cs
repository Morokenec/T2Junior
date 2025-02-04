using T2JuniorAPI.DTOs;

namespace T2JuniorAPI.Services
{
    public interface IOrganizationService
    {
        Task<List<OrganizationDto>> GetAllOrganizationsAsync();
        Task<string> CreateOrganization(OrganizationDto organization);
        Task<string> UpdateOrganization(Guid id, OrganizationDto organization);
        Task<string> DeleteOrganization(Guid id);
    };
}