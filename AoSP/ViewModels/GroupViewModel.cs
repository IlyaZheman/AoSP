namespace AoSP.ViewModels;

public class GroupViewModel
{
    public string? GroupId { get; set; }
    public string? GroupTitle { get; set; }
    public List<UserViewModel> Students { get; set; } = new();
    public List<SubjectViewModel> Subjects { get; set; } = new();
}