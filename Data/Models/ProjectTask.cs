using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; } = null!;
    }
}
