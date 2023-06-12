namespace AoSP.Entities;

public class Mark
{
    public string Id { get; set; }
    public string Place { get; set; }
    public DateTime DateTime { get; set; }

    public string? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}