using AoSP.Enums;

namespace AoSP.ViewModels;

public class UserViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Login { get; set; }
    public Role Role { get; set; }
}