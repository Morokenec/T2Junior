using System.Threading.Tasks;

public interface IAccountService
{
    Task<string> RegisterUserAsync(RegisterUserDto registerUserDto);
}
