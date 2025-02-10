namespace T2JuniorAPI.Services.Tokens
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
