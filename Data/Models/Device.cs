using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public int PlantId { get; set; }
        [Required]
        public Plant Plant { get; set; }
    }
}
