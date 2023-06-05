namespace AoSP.ViewModels;

public class SubjectViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<SubjectTaskViewModel> SubjectTasks { get; set; }
}