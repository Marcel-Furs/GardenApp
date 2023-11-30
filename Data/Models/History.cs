using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public int DeviceId { get; set; }
        [Required]
        public Device Device { get; set; } = null!;
    }
}
