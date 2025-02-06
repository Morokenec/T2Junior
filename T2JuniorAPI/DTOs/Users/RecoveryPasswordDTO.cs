using System.ComponentModel.DataAnnotations;

namespace T2JuniorAPI.DTOs.Users
{
    public class RecoveryPasswordDTO
    {
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
    }
}
