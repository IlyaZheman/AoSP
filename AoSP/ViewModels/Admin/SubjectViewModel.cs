namespace AoSP.ViewModels.Admin;

public class SubjectViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public UserViewModel Teacher { get; set; }
    public List<SubjectTaskViewModel> SubjectTasks { get; set; } = new();
    public List<MarkViewModel> Marks { get; set; } = new();
}