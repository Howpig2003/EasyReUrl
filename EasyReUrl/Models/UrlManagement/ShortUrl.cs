using System.ComponentModel.DataAnnotations;
namespace EasyReUrl.Models.UrlManagement
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2000)]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortenedUrl { get; set; }

        public int UsageCount { get; set; } = 0;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}




