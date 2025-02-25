using T2JuniorAPI.DTOs.WallTypes;

namespace T2JuniorAPI.Services.WallTypes
{
    public interface IWallTypeService
    {
        Task<WallTypeDTO> GetOrCreateWallTypeAsync(CreateWallTypeDTO createWallTypeDto);
        Task<List<WallTypeDTO>> GetAllWallTypesAsync();
        Task<bool> DeleteWallTypeAsync(Guid id);
    }
}
