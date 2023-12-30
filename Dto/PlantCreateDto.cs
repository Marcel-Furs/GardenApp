namespace GardenApp.API.Dto
{
    public class PlantCreateDto
    {
        public string PlantName { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public string PathImage { get; set; } = null!;
        public int DeviceId { get; set; }
        public int PlantProfileId { get; set; }
    }
}
