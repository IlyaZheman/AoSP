namespace AoSP.Entities;

public class Group
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<User> Users { get; set; } = new List<User>();
}