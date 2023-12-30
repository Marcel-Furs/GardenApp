using AutoMapper;
using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.UnitOfWork;
using GardenApp.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("{id}/days")]
        public async Task<IActionResult> GetDays(int id)
        {
            try
            {
                var today = DateTime.Now.Date;
                var days = await unitOfWork.CalendarRepository
                                          .GetAll(calendar => calendar.UserId == id && calendar.IsActive == false);

                var orderedDays = days.Where(calendar => calendar.EventDate >= today)
                                      .OrderBy(calendar => calendar.EventDate)
                                      .Take(3)
                                      .ToList();

                return Ok(mapper.Map<List<CalendarDto>>(orderedDays));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpGet("{id}/{city}/alert")]
        public async Task<IActionResult> GetData(int id, string city)
        {
            try
            {
                var endDate = DateTime.Now;
                var startDate = endDate.AddDays(-7);

                var data = await unitOfWork.WeatherMeasurementRepository
                    .GetAll(model => model.UserId == id && model.City == city && model.Date >= startDate && model.Date <= endDate);

                if (!data.Any())
                {
                    return NotFound($"Nie znaleziono danych dla miasta: {city}");
                }

                var totalPrecipitation = data.Sum(model => model.Precipitation);

                var alert = totalPrecipitation > 20 ? 1 : 0;

                return Ok(alert);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania danych pogodowych: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpGet("{id}/latedays")]
        public async Task<IActionResult> GetLateDays(int id)
        {
            try
            {
                var today = DateTime.Now.Date;
                var days = await unitOfWork.CalendarRepository
                                          .GetAll(calendar => calendar.UserId == id && calendar.IsActive == false);

                var orderedDays = days.Where(calendar => calendar.EventDate < today)
                                      .OrderByDescending(calendar => calendar.EventDate) 
                                      .Take(3)
                                      .ToList();

                return Ok(mapper.Map<List<CalendarDto>>(orderedDays));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpPost("{dayId}/updatelateday")]
        public async Task<IActionResult> UpdateLDayStatus(int dayId, [FromBody] CalendarDto calendarDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var day = await unitOfWork.CalendarRepository.Get(dayId);
                if (day == null)
                {
                    return NotFound($"Dzień o ID {dayId} nie został znaleziony.");
                }

                day.IsActive = calendarDto.IsActive;
                unitOfWork.CalendarRepository.Update(day);

                return Ok(new { Message = "Status aktywności zaktualizowany." });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas aktualizacji statusu dnia: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
    }
}
