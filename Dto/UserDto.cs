using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Dto
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
