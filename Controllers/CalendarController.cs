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
        public CalendarController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("create")]
        public async Task<IActionResult> TestCreate([FromBody] CalendarDto calendarDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //DateTime trimmedDate = TrimTime(calendarDto.EventDate);
                calendarDto.EventDate = calendarDto.EventDate.Date;

                await unitOfWork.CalendarRepository.Create(new Calendar
                {
                    Description = calendarDto.Description,
                    EventDate = calendarDto.EventDate,
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
        //private DateTime TrimTime(DateTime dateTime)
        //{
        //    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        //}

    }
}
