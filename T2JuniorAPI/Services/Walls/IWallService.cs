using T2JuniorAPI.DTOs.Walls;

namespace T2JuniorAPI.Services.Walls
{
    public interface IWallService
    {
        Task<WallDTO> CreateWallAsync(Guid idOwner);
        Task DeleteWallAsync(Guid idOwner);
        Task<WallDTO> GetWallByIdOwnerAsync(Guid idOwner);
    }
}
