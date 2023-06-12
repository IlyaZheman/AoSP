using AoSP.ViewModels.Student;

namespace AoSP.ViewModels.Teacher;

public class TeacherGradeViewModel
{
    public UserViewModel Teacher { get; set; }
    public List<StudentGradeViewModel> Students { get; set; }
}