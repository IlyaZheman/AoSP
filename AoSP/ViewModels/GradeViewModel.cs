namespace AoSP.ViewModels;

public class GradeViewModel
{
    public int SelectedGroupId { get; set; }
    public List<GroupViewModel> Groups { get; set; } = new();
}