using AoSP.Enums;

namespace AoSP.Entities;

public class User
{
    public string Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public Role Role { get; set; }
    public virtual ICollection<PersonalSubject> PersonalSubjects { get; set; }

    public string? GroupId { get; set; }
    public virtual Group Group { get; set; }
    public string? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}