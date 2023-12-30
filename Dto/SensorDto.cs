namespace GardenApp.API.Dto
{
    public class SensorDto
    {
        public int Id { get; set; }
        public int SensorTypeId { get; set; }
        public string SensorValue { get; set; }
        public int DeviceId { get; set; }
    }
}
