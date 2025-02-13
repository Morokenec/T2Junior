using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class UserProfileDTO
    {
        public int IdUser { get; set; }

        public string RoleName { get; set; } = null!;

        public string OrganizationName { get; set; } = null!;

        public string FirstName { get; set; } = "Дмитрий";

        public string LastName { get; set; } = "Ушаков";

        public string FullName => $"{FirstName} {LastName}";

        public string? Patronymic { get; set; }

        public string? Post { get; set; }

        public DateTime? Birthday { get; set; }

        public sbyte Sex { get; set; }

        public int AccumulatedPoints { get; set; } = 5;
    }
}
