using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services
{
    public class ClubRoleService : IClubRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClubRoleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClubRolesDTO>> GetAllClubRolesAsync()
        {
            return await _context.ClubRoles
                .Where(cr => cr.IsDelete ==  false)
                .ProjectTo<ClubRolesDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ClubRolesDTO> GetClubRoleByIdAsync(Guid id)
        {
            var clubRole = await _context.ClubRoles
                .Where(cr => cr.Id == id)
                .ProjectTo<ClubRolesDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (clubRole == null)
            {
                throw new ApplicationException("Club role not found");
            }

            return clubRole;
        }

        public async Task<ClubRolesDTO> CreateClubRoleAsync(ClubRolesDTO clubRoleDto)
        {
            if (string.IsNullOrWhiteSpace(clubRoleDto.Name))
                throw new ApplicationException("Name is reqired");

            var clubRole = _mapper.Map<ClubRole>(clubRoleDto);
            _context.ClubRoles.Add(clubRole);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClubRolesDTO>(clubRole);
        }

        public async Task<ClubRolesDTO> UpdateClubRoleAsync(Guid id, ClubRolesDTO clubRoleDto)
        {
            if (string.IsNullOrWhiteSpace(clubRoleDto.Name))
                throw new ApplicationException("Name is required");

            var clubRole = await _context.ClubRoles.FindAsync(id) ?? throw new ApplicationException("Club role not found");

            _mapper.Map(clubRoleDto, clubRole);
            clubRole.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return _mapper.Map<ClubRolesDTO>(clubRole);
        }

        public async Task<bool> DeleteClubRoleAsync(Guid id)
        {
            var clubRole = await _context.ClubRoles.FindAsync(id);
            if (clubRole == null)
            {
                throw new ApplicationException("Club role not found");
            }

            clubRole.IsDelete = true;
            clubRole.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}