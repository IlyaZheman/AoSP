using System.ComponentModel.DataAnnotations;

namespace AoSP.ViewModels;

public class PasswordViewModel
{
    public string Login { get; set; }

    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}