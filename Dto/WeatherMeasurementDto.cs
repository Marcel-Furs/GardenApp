namespace GardenApp.API.Dto
{
    public class WeatherMeasurementDto
    {
        public string City { get; set; }
        public DateTime Date { get; set; }
        public int MeasurementTime { get; set; }
        public double Temperature { get; set; }
        public double Precipitation { get; set; }
        public int UserId { get; set; }
    }
}
