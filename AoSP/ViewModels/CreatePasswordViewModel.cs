using System.ComponentModel.DataAnnotations;

namespace AoSP.ViewModels;

public class CreatePasswordViewModel
{
    public string Login { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Укажите пароль")]
    [MinLength(3, ErrorMessage = "Пароль должен иметь длину больше 3 символов")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; }
}