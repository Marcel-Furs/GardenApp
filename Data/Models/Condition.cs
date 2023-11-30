using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Condition
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public int PlantId { get; set; }
        [Required]
        public Plant Plant { get; set; } = null!;
    }
}
