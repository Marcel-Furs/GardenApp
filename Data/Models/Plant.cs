using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlantName { get; set; } = null!;
        public string? PathImage { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public int PlantProfileId { get; set; } 
        public bool IsActive { get; set; }
        public int? DiaryId { get; set; }
        public Diary? Diary { get; set; }
        public virtual PlantProfile PlantProfile { get; set; }
    }
}
