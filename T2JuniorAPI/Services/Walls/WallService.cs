using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.Walls;
using T2JuniorAPI.DTOs.WallTypes;
using T2JuniorAPI.Entities;
using T2JuniorAPI.Services.WallTypes;

namespace T2JuniorAPI.Services.Walls
{
    public class WallService : IWallService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWallTypeService _typeService;

        public WallService(ApplicationDbContext context, IMapper mapper, IWallTypeService typeService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _typeService = typeService;
            _userManager = userManager;
        }

        public async Task<WallDTO> CreateWallAsync(Guid idOwner)
        {
            var existingWall = await _context.Walls.FirstOrDefaultAsync(w => w.IdUserOwner == idOwner || w.IdClubOwner == idOwner);

            if (existingWall != null)
                throw new ApplicationException("Wall for this owner alredy exists");
            // Определение типа владельца
            string wallTypeName = await DetermineWallTypeAsync(idOwner);
            var wallTypeDTO = await _typeService.GetOrCreateWallTypeAsync(new CreateWallTypeDTO { Name = wallTypeName });

            var wall = _mapper.Map<Wall>(new CreateWallDTO { IdOwner = idOwner, IdType = wallTypeDTO.Id });

            if (wallTypeName == "UserWall")
                wall.IdUserOwner = idOwner;
            else if (wallTypeName == "ClubWall")
                wall.IdClubOwner = idOwner;
            else
                throw new ApplicationException("Owner Not Found");


            await _context.Walls.AddAsync(wall);
            await _context.SaveChangesAsync();

            return _mapper.Map<WallDTO>(wall);
        }

        private async Task<string> DetermineWallTypeAsync(Guid idOwner)
        {
            var user = await _context.Users.FindAsync(idOwner);
            if (user != null)
                return "UserWall";

            var club = await _context.Clubs.FindAsync(idOwner);
            if (club != null)
                return "ClubWall";

            throw new ApplicationException("Owner Type Not Found");
        }

        public async Task DeleteWallAsync(Guid idOwner)
        {
            var wall = await _context.Walls.FirstOrDefaultAsync(w => 
                (w.IdUserOwner == idOwner || w.IdClubOwner == idOwner) && !w.IsDelete)
                ?? throw new ApplicationException($"Wall not found");
            wall.IsDelete = true;
            wall.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task<WallDTO> GetWallByIdOwnerAsync(Guid idOwner)
        {
            var wall = await _context.Walls
                .Include(w => w.IdTypeNavigation)
                .Include(w => w.ClubOwner)
                .Include(w => w.UserOwner)
                .FirstOrDefaultAsync(w => w.IdUserOwner == idOwner || w.IdClubOwner == idOwner);

            if (wall == null)
                return null;

            return _mapper.Map<WallDTO> (wall);
        }
    }
}
