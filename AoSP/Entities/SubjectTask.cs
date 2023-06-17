namespace AoSP.Entities;

public class SubjectTask
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<PersonalSubjectTask> PersonalSubjectTasks { get; set; } = new List<PersonalSubjectTask>();

    public string? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}