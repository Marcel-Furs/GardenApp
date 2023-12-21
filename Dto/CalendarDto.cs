using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Dto
{
    public class CalendarDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
