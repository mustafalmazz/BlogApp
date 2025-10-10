using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string? UserName { get; set; }
        [Required]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }
        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        [StringLength(15, ErrorMessage = "Max 15 karakter girebilirsiniz!")]
        public string? Password { get; set; }
        [Required]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
