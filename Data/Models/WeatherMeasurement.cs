using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenApp.API.Data.Models
{
    public class WeatherMeasurement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int MeasurementTime { get; set; }
        [Required]
        public double Temperature { get; set; } 
        public double Precipitation { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}