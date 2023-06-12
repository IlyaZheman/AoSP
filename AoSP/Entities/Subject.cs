namespace AoSP.Entities;

public class Subject
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public virtual User Teacher { get; set; }
    public virtual ICollection<SubjectTask> SubjectTasks { get; set; } = new List<SubjectTask>();
    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();
    public virtual ICollection<PersonalSubject> PersonalSubjects { get; set; } = new List<PersonalSubject>();

    public string? GroupId { get; set; }
    public virtual Group Group { get; set; }
}