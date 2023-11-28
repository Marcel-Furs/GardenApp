using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlantName { get; set; }
    }
}
