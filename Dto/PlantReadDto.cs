namespace GardenApp.API.Dto
{
    public class PlantReadDto
    {
        public int Id { get; set; }
        public string PlantName { get; set; } = null!;
        public string PathImage { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public string PlantProfileName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
