namespace AoSP.Entities;

public class SubjectTask
{
    public int Id { get; set; }
    public string? Description { get; set; }

    public int? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}