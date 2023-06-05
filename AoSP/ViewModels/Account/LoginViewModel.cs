using System.ComponentModel.DataAnnotations;

namespace AoSP.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину больше 3 символов")]
        public string Login { get; set; }
    }
}