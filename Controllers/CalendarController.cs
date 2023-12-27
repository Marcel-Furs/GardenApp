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
    public class CalendarController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CalendarController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CalendarDto calendarDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                calendarDto.EventDate = calendarDto.EventDate.Date;

                await unitOfWork.CalendarRepository.Create(new Calendar
                {
                    Description = calendarDto.Description,
                    EventDate = calendarDto.EventDate,
                    IsActive = calendarDto.IsActive,
                    UserId = calendarDto.UserId
                });

                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpGet("{id}/days")]
        public async Task<IActionResult> GetDays(int id)
        {
            try
            {
                var days = await unitOfWork.CalendarRepository.GetAll(calendar => calendar.UserId == id && calendar.IsActive == false);

                var orderedDays = days.OrderBy(calendar => calendar.EventDate).ToList();


                return Ok(mapper.Map<List<CalendarDto>>(orderedDays));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpPost("{dayId}/update")]
        public async Task<IActionResult> UpdateDayStatus(int dayId, [FromBody] CalendarDto calendarDto)
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
