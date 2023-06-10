namespace AoSP.Entities;

public class Group
{
    public string Id { get; set; }
    public string? Title { get; set; }
    public virtual ICollection<User> Students { get; set; }
    public virtual ICollection<Subject> Subjects { get; set; }
}