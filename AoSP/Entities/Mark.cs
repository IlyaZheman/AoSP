namespace AoSP.Entities;

public class Mark
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Place { get; set; }
    
    public int? SubjectId { get; set; }
    public virtual Subject Subject { get; set; }
}