namespace AoSP.Entities;

public class Subject
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? TeacherId { get; set; }
    public virtual List<SubjectTask> SubjectTasks { get; set; }
    public virtual List<Mark> Marks { get; set; }

    public int? GroupId { get; set; }
    public virtual Group Group { get; set; }
}