namespace AoSP.ViewModels.Student;

public class PersonalSubjectViewModel
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public UserViewModel Teacher { get; set; }
    public List<PersonalSubjectTaskViewModel> PersonalSubjectTasks { get; set; }
}