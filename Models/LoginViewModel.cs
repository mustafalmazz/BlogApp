using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel 
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string?  Email { get; set; }
        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [StringLength(15,ErrorMessage = "Max 15 karakter girebilirsiniz!")]
        public string? Password { get; set; }
    }
}
