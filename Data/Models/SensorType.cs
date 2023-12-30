using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class SensorType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // Nazwa typu czujnika, np. "Temperatura", "Wilgotność"

        // Możesz również dodać dodatkowe właściwości, które opisują typ czujnika, np. jednostki pomiarowe
        [Required]
        public string MeasurementUnit { get; set; } // Jednostka pomiarowa, np. "°C", "%", itp.

        // Relacja z Sensor, jeśli jeden SensorType może być przypisany do wielu Sensorów
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
