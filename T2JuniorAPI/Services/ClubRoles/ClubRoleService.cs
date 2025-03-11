using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.ClubRoles;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.ClubRoles
{
    /// <summary>
    /// Сервис для работы с ролями в клубе, включая получение, создание, обновление и удаление ролей, а также предоставление списка всех ролей в клубе.
    /// </summary>
    public class ClubRoleService : IClubRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для инициализации сервисов ApplicationDbContext и IMapper
        /// </summary>
        /// <param name="context">Доступ к базе данных</param>  
        /// <param name="mapper">Сопоставление данных между объектами</param>
        public ClubRoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение списка всех неудалённых ролей
        /// </summary>
        public async Task<List<ClubRolesDTO>> GetAllClubRolesAsync()
        {
            return await _context.ClubRoles
                .Where(cr => cr.IsDelete == false)
                .ProjectTo<ClubRolesDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Получение роли в клубе по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор роли.</param>
        /// <returns> DTO-объект роли в клубе или null, если роль не найдена.</returns>
        public async Task<ClubRolesDTO> GetClubRoleByIdAsync(Guid id)
        {
            var clubRole = await _context.ClubRoles
                .Where(cr => cr.Id == id)
                .ProjectTo<ClubRolesDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (clubRole == null)
            {
                return null;
            }

            return clubRole;
        }

        /// <summary>
        /// Создание новой роли в клубе.
        /// </summary>
        /// <param name="clubRoleDto">Объект с новыми данными для новой роли.</param>
        /// <returns>DTO-объект созданной роли.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если имя роли не указано или роль с таким именем уже существует.</exception>
        public async Task<ClubRolesDTO> CreateClubRoleAsync(ClubRolesDTO clubRoleDto)
        {
            if (string.IsNullOrWhiteSpace(clubRoleDto.Name))
                throw new ApplicationException("Name is reqired");

            if (await _context.ClubRoles.AnyAsync(cr => cr.Name == clubRoleDto.Name))
                throw new ApplicationException("This role alredy exists");


            var clubRole = _mapper.Map<ClubRole>(clubRoleDto);
            _context.ClubRoles.Add(clubRole);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClubRolesDTO>(clubRole);
        }

        /// <summary>
        /// Обновление информации о роли в клубе.
        /// </summary>
        /// <param name="id">Уникальный идентификатор роли.</param>
        /// <param name="clubRoleDto">DTO-объект с новыми данными для обновления.</param>
        /// <returns>DTO-объект обновлённой роли или null, если роль не найдена.</returns>
        /// <exception cref="ApplicationException">Выбрасывается, если имя роли не указано.</exception>
        public async Task<ClubRolesDTO> UpdateClubRoleAsync(Guid id, ClubRolesDTO clubRoleDto)
        {
            if (string.IsNullOrWhiteSpace(clubRoleDto.Name))
                return null;

            var clubRole = await _context.ClubRoles.FindAsync(id);
            if (clubRole == null)
                return null;

            _mapper.Map(clubRoleDto, clubRole);
            clubRole.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<ClubRolesDTO>(clubRole);
        }

        /// <summary>
        /// Удаление роли в клубе.
        /// </summary>
        /// <param name="id">Уникальный идентификатор роли</param>
        /// <returns>Возвращает true если роль была удалена, иначе false </returns>
        public async Task<bool> DeleteClubRoleAsync(Guid id)
        {
            var clubRole = await _context.ClubRoles.FindAsync(id);
            if (clubRole == null)
                return false;

            clubRole.IsDelete = true;
            clubRole.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}