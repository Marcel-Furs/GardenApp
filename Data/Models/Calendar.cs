using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Calendar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; } = false;
        public int UserId { get; set; }
        public int? DiaryId { get; set; }
        public Diary? Diary { get; set; }
        [Required]
        public User User { get; set; } = null!;
    }
}
