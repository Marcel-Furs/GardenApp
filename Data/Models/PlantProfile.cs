using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class PlantProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProfileName { get; set; } = null!;
        public ICollection<Plant> Plants { get; set; } = null!;
    }
}
