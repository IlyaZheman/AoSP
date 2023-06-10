namespace AoSP.Entities;

public class Subject
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public string? TeacherId { get; set; }
    public virtual User Teacher { get; set; }
    public virtual List<SubjectTask> SubjectTasks { get; set; }
    public virtual List<Mark> Marks { get; set; }

    public string? GroupId { get; set; }
    public virtual Group Group { get; set; }
}