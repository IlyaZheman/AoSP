namespace AoSP.ViewModels.Teacher;

public class StudentViewModel : UserViewModel
{
    public List<SubjectTaskViewModel> Tasks { get; set; } = new();
}