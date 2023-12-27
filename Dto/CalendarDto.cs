using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Dto
{
    public class CalendarDto
    {   
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }
        [Required]
        public bool IsActive { get; set; } = false;
        [Required]
        public int UserId { get; set; }
    }
}
