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
    /// <summary>
    /// Сервис для работы со стенами
    /// </summary>
    public class WallService : IWallService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IWallTypeService _typeService;

        /// <summary>
        /// Конструктор для внедрения зависимостей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="mapper">Маппер для преобразования объектов</param>
        /// <param name="typeService">Сервис для работы с типами стен</param>
        /// <param name="userManager">Менеджер пользователей</param>
        public WallService(ApplicationDbContext context, IMapper mapper, IWallTypeService typeService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _typeService = typeService;
            _userManager = userManager;
        }

        /// <summary>
        /// Определяет тип стены в зависимости от типа владельца
        /// </summary>
        /// <param name="idOwner">Id владельца стены</param>
        /// <returns>Название типа стены</returns>
        public async Task<WallDTO> CreateWallAsync(Guid idOwner)
        {
            string wallTypeName = await DetermineWallTypeAsync(idOwner);
            var wallTypeDTO = await _typeService.GetOrCreateWallTypeAsync(new CreateWallTypeDTO { Name = wallTypeName });

            var owner = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == idOwner);
            var wallType = await _context.WallTypes.FindAsync(wallTypeDTO.Id);
            if (owner == null)
                throw new ApplicationException("User is Empty");

            if (wallType == null)
                throw new ApplicationException("Type is Empty");


            var wall = _mapper.Map<Wall>(new CreateWallDTO { IdOwner = idOwner, IdType = wallTypeDTO.Id });

            await _context.Walls.AddAsync(wall);
            await _context.SaveChangesAsync();

            return _mapper.Map<WallDTO>(wall);
        }

        /// <summary>
        /// Определяет тип стены в зависимости от типа владельца
        /// </summary>
        /// <param name="idOwner">Id владельца стены</param>
        /// <returns>Название типа стены</returns>
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

        /// <summary>
        /// Удаляет стену указанного владельца
        /// </summary>
        /// <param name="idOwner">Id владельца стены</param>
        /// <returns>Сообщение о результате операции</returns>
        public async Task DeleteWallAsync(Guid idOwner)
        {
            var wall = await _context.Walls.FirstOrDefaultAsync(w => w.IdOwner == idOwner && !w.IsDelete);
            
            if (wall == null)
                throw new ApplicationException($"Wall not found");

            wall.IsDelete = true;
            wall.UpdateDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возвращает данные о стене указанного владельца
        /// </summary>
        /// <param name="idOwner">Id владельца стены</param>
        /// <returns>Данные о стене</returns>
        public async Task<WallDTO> GetWallByIdOwnerAsync(Guid idOwner)
        {
            var wall = await _context.Walls
                .Include(w => w.IdTypeNavigation)
                .Include(w => w.UserOwner)
                .Include(w => w.ClubOwner)
                .FirstOrDefaultAsync(w => w.IdOwner == idOwner);

            if (wall == null)
                return null;

            return _mapper.Map<WallDTO> (wall);
        }
    }
}
