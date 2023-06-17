namespace AoSP.ViewModels.Student;

public class PersonalSubjectTaskViewModel
{
    public string? Id { get; set; }
    public string? TaskTitle { get; set; }
    public int? Score { get; set; }
    public IFormFile? File { get; set; }
}