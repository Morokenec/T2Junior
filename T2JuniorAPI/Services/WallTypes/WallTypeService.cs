using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using T2JuniorAPI.Data;
using T2JuniorAPI.DTOs.WallTypes;
using T2JuniorAPI.Entities;

namespace T2JuniorAPI.Services.WallTypes
{
    /// <summary>
    /// Сервис для работы с типами стен
    /// </summary>
    public class WallTypeService : IWallTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        /// <summary>
        /// Конструктор для внедрения зависимостей
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        /// <param name="mapper">Маппер для преобразования объектов</param>
        public WallTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Возвращает тип стены по названию, или создает новый тип, если он не найден
        /// </summary>
        /// <param name="createWallTypeDTO">Данные для создания или обновления типа стены</param>
        /// <returns>Данные о типе стены</returns>
        public async Task<WallTypeDTO> GetOrCreateWallTypeAsync(CreateWallTypeDTO createWallTypeDTO)
        {
            if (string.IsNullOrEmpty(createWallTypeDTO.Name))
            {
                throw new ApplicationException("Name is requred");
            }

            var existingWallType = await _context.WallTypes
                .FirstOrDefaultAsync(wt => wt.Name == createWallTypeDTO.Name);

            if (existingWallType != null)
                return _mapper.Map<WallTypeDTO>(existingWallType);

            var newWallType = _mapper.Map<WallType>(createWallTypeDTO);

            await _context.WallTypes.AddAsync(newWallType);
            await _context.SaveChangesAsync();

            return _mapper.Map<WallTypeDTO>(newWallType);
        }

        /// <summary>
        /// Возвращает список всех неудаленных типов стен
        /// </summary>
        /// <returns>Коллекция типов стен</returns>
        public async Task<List<WallTypeDTO>> GetAllWallTypesAsync()
        {
            return await _context.WallTypes
                .Where(wt => !wt.IsDelete)
                .ProjectTo<WallTypeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        /// <summary>
        /// Помечает тип стены как удаленную
        /// </summary>
        /// <param name="id">Id удаляемого типа стены</param>
        /// <returns>True, если операция прошла успешно, иначе false</returns>
        public async Task<bool> DeleteWallTypeAsync(Guid id)
        {
            var wallType = await _context.WallTypes.FindAsync(id);
            if (wallType == null)
                return false;

            wallType.IsDelete = true;
            wallType.UpdateDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
