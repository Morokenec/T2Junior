public partial class RegisterModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Patronymic { get; set; }
    public string? Organization { get; set; }
    public string? Post { get; set; }
    public int? Age { get; set; }
    public sbyte Sex { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
}