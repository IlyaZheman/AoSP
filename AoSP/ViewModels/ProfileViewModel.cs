using System.ComponentModel.DataAnnotations;
using AoSP.Enums;

namespace AoSP.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public Role Role { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        public string Login { get; set; }
    }
}