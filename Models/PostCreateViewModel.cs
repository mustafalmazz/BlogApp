using BlogApp.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostCreateViewModel
    {
        public int PostId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Post Başlığı")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Post İçeriği")]
        public string? Content { get; set; }

        [Required]
        [StringLength(700)]
        [Display(Name = "Post Açıklaması")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Post Url")]
        public string? Url { get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
        public List<Tag> Tags { get; set; } = new();

    }
}
