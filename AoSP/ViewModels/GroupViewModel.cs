namespace AoSP.ViewModels;

public class GroupViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<UserViewModel> Users { get; set; }
}