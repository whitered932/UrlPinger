using System.ComponentModel.DataAnnotations;

namespace UrlPinger.Models
{
    public class RemoteAddress
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        [Required, Url]
        public string Url { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
