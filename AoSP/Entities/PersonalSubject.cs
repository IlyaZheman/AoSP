namespace AoSP.Entities;

public class PersonalSubject
{
    public string Id { get; set; }
    public virtual ICollection<PersonalSubjectTask> PersonalSubjectTasks { get; set; }

    public string? UserId { get; set; }
    public virtual User User { get; set; }
    public string? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}