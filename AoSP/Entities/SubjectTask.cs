namespace AoSP.Entities;

public class SubjectTask
{
    public string Id { get; set; }
    public string? Description { get; set; }

    public string? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}