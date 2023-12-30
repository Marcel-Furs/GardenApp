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
        public virtual User User { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
