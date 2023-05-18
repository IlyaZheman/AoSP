using System.ComponentModel.DataAnnotations;

namespace AoSP.ViewModels
{
    public class ProfileViewModel
    {
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Укажите имя")]
        public string Login { get; set; }
    }
}