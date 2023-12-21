namespace GardenApp.API.Dto
{
    public class PlantCreateDto
    {
        public string PlantName { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
