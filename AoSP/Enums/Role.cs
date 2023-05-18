using System.ComponentModel.DataAnnotations;

namespace AoSP.Enums;

public enum Role
{
    [Display(Name = "Студент")]
    Student = 0,
    [Display(Name = "Преподаватель")]
    Teacher = 1,
    [Display(Name = "Админ")]
    Admin = 2,
}