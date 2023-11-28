using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
