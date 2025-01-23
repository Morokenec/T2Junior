namespace T2Junior.Models
{
    public class UserProfileDTO
    {
        public int IdUser { get; set; }

        public string RoleName { get; set; } = null!;

        public string OrganizationName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Patronymic { get; set; }

        public string? Post { get; set; }

        public int? Age { get; set; }

        public sbyte Sex { get; set; }

        public int AccumulatedPoints { get; set; }
    }
}
