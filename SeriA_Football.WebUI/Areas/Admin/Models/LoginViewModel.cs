using System.ComponentModel.DataAnnotations;

namespace SeriA_Football.WebUI.Areas.Admin.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = "";

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
