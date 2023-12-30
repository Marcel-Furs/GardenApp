using System.ComponentModel.DataAnnotations;

namespace GardenApp.API.Data.Models
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SensorTypeId { get; set; }
        public virtual SensorType SensorType { get; set; }
        [Required]
        public string SensorValue { get; set; } // Ostatnia odczytana wartość
        public int DeviceId { get; set; } // Klucz obcy do Device
        public virtual Device Device { get; set; }
    }
}
