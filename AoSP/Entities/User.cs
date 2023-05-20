using AoSP.Enums;

namespace AoSP.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public Role Role { get; set; }
    public Profile Profile { get; set; }
}