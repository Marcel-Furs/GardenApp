namespace GardenApp.API.Dto
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public List<SensorDto> Sensors { get; set; }
        public int UserId { get; set; }
    }
}
