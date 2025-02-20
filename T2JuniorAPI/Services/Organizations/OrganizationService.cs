using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Organizations;
using T2JuniorAPI.Services.Organizations;

/// <summary>
/// Сервис для работы с организациями
/// </summary>
public class OrganizationService : IOrganizationService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор для внедрения зависимостей
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    /// <param name="mapper">Маппер для преобразования объектов</param>
    public OrganizationService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Возвращает список всех неудаленных организаций
    /// </summary>
    /// <returns>Коллекция организаций</returns>
    public async Task<List<OrganizationDto>> GetAllOrganizationsAsync()
    {
        var organizations = await _context.Organizations
            .Where(o => o.IsDelete == false)
            .ToListAsync();
        return _mapper.Map<List<OrganizationDto>>(organizations);
    }

    /// <summary>
    /// Создает новую организацию, если такой уже не существует
    /// </summary>
    /// <param name="organization">Данные о создаваемой организации</param>
    /// <returns>Сообщение о результате операции</returns>
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
        catch (DbUpdateException)
        {
            return null;
        }

        return "success";
    }

    /// <summary>
    /// Обновляет данные организации
    /// </summary>
    /// <param name="id">Id обновляемой организации</param>
    /// <param name="organizationDto">Данные обновляемой организации</param>
    /// <returns>Сообщение о результате операции</returns>
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
        catch (DbUpdateException)
        {
            return null;
        }

        return "success";
    }

    /// <summary>
    /// Помечает организацию как удаленную
    /// </summary>
    /// <param name="id">Id удаляемой организации</param>
    /// <returns>Сообщение о результате операции</returns>
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
        catch (DbUpdateException)
        {
            return null;
        }

        return "Success";
    }
}
