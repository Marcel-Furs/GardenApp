using AutoMapper;
using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.UnitOfWork;
using GardenApp.API.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public ChartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddWeatherData([FromBody] WeatherMeasurementDto weatherMeasurementDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await unitOfWork.WeatherMeasurementRepository.Create(new WeatherMeasurement
                {
                    City = weatherMeasurementDto.City,
                    Date = weatherMeasurementDto.Date,
                    MeasurementTime = weatherMeasurementDto.MeasurementTime,
                    Temperature = weatherMeasurementDto.Temperature,
                    Precipitation = weatherMeasurementDto.Precipitation,
                    UserId = weatherMeasurementDto.UserId

                });

                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu danych miasta: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("{id}/{city}/data")]
        public async Task<IActionResult> Getdata(int id, string city)
        {
            try
            {
                var data = await unitOfWork.WeatherMeasurementRepository
                    .GetAll(model => model.UserId == id && model.City == city);

                if (!data.Any())
                {
                    return NotFound($"Nie znaleziono danych dla miasta: {city}");
                }

                var groupedData = data
                           .GroupBy(model => model.Date)
                           .Select(g =>
                           {
                               var averageTemperature = g.Average(model => model.Temperature);
                               var roundedAverageTemperature = Math.Round(averageTemperature, 1); // Zaokrąglenie do jednego miejsca po przecinku
                               var representativeRecord = g.OrderByDescending(model => model.MeasurementTime).FirstOrDefault();
                               representativeRecord.Temperature = roundedAverageTemperature; // Nadpisujemy temperaturę średnią
                               return representativeRecord;
                           })
                           .OrderByDescending(model => model.Date) // sortujemy od najnowszych do najstarszych dni
                           .Take(7) // Bierzemy 7 najnowszych dni
                           .OrderBy(model => model.Date) // sortujemy od najstarszych do najnowszych dni
                           .ToList();

                return Ok(mapper.Map<List<WeatherMeasurementDto>>(groupedData));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
    }
}
