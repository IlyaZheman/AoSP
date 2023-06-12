namespace AoSP.ViewModels;

public class GradeViewModel
{
    public string SelectedGroupId { get; set; }
    public List<GroupViewModel> Groups { get; set; } = new();
}