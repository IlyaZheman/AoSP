namespace AoSP.ViewModels.Teacher;

public class SubjectViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public List<StudentViewModel> Students { get; set; } = new();
}