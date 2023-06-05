namespace AoSP.Entities;

public class PersonalSubject
{
    public int Id { get; set; }
    public int? SubjectId { get; set; }
    public virtual ICollection<PersonalSubjectTask> PersonalSubjectTasks { get; set; }

    public int? UserId { get; set; }
    public virtual User? User { get; set; }
}