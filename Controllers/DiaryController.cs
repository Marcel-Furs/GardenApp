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
    public class DiaryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private static readonly string MediaDir = "./media";
        public DiaryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("{id}/getPlants")]
        public async Task<IActionResult> GetPlants(int id)
        {
            try
            {
                var devices = await unitOfWork.DeviceRepository.GetAll(x => x.UserId == id);
                var plantProfiles = await unitOfWork.PlantProfileRepository.GetAll();

                var deviceIds = devices.Select(d => d.Id).ToList();
                var plants = await unitOfWork.PlantRepository.GetAll(x => deviceIds.Contains(x.DeviceId));
                var plantReadDtos = new List<PlantReadDto>();

                foreach (var plant in plants)
                {
                    var device = devices.FirstOrDefault(d => d.Id == plant.DeviceId);
                    var plantProfile = plantProfiles.FirstOrDefault(pp => pp.Id == plant.PlantProfileId);

                    var plantReadDto = mapper.Map<PlantReadDto>(plant);
                    plantReadDto.Id = plant.Id;
                    plantReadDto.DeviceName = device?.DeviceName;
                    plantReadDto.PlantProfileName = plantProfile?.ProfileName;
                    // Ustaw wartość IsActive na podstawie właściwości IsActive z obiektu Plant
                    plantReadDto.IsActive = plant.IsActive;

                    plantReadDtos.Add(plantReadDto);
                }

                return Ok(plantReadDtos);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania roślin: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }


        [HttpGet("{name}/Image")]
        public IActionResult GetImage(string name)
        {
            var newName = RemoveMediaPathPrefix(name);
            var path = Path.Combine(MediaDir, newName);
            if (System.IO.File.Exists(path))
            {
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/" + Path.GetExtension(path));
            }

            return NotFound();
        }
        private static string RemoveMediaPathPrefix(string path)
        {
            string prefix = ".%2Fmedia\\";

            if (path.StartsWith(prefix))
            {
                return path.Substring(prefix.Length);
            }

            return path;
        }

        [HttpGet("{id}/days")]
        public async Task<IActionResult> GetInActiveDays(int id)
        {
            try
            {
                var days = await unitOfWork.CalendarRepository.GetAll(calendar => calendar.UserId == id && calendar.IsActive == true);

                var orderedDays = days.OrderBy(calendar => calendar.EventDate).ToList();


                return Ok(mapper.Map<List<CalendarDto>>(orderedDays));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpPost("CreateDiary")]
        public async Task<IActionResult> CreateDiary([FromBody] DiaryDto diaryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newDiary = new Diary
                {
                    EntryDate = diaryDto.EntryDate,
                    Description = diaryDto.Description,
                    UserId = diaryDto.UserId,
                    Plants = new List<Plant>(),
                    Calendars = new List<Calendar>()
                };

                // Dodajemy rośliny do dziennika
                if (diaryDto.PlantIds != null && diaryDto.PlantIds.Any())
                {
                    var plants = await unitOfWork.PlantRepository.GetAll(p => diaryDto.PlantIds.Contains(p.Id));
                    foreach (var plant in plants)
                    {
                        newDiary.Plants.Add(plant);
                    }
                }

                // Dodajemy wydarzenia do dziennika
                if (diaryDto.CalendarIds != null && diaryDto.CalendarIds.Any())
                {
                    var calendars = await unitOfWork.CalendarRepository.GetAll(c => diaryDto.CalendarIds.Contains(c.Id));
                    foreach (var calendar in calendars)
                    {
                        newDiary.Calendars.Add(calendar);
                    }
                }

                await unitOfWork.DiaryRepository.Create(newDiary);

                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu dziennika: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpGet("{userId}/diaries")]
        public async Task<IActionResult> GetAllDiaries(int userId)
        {
            try
            {
                var diaries = await unitOfWork.DiaryRepository.GetAll(diary => diary.UserId == userId);

                if (diaries == null || !diaries.Any())
                {
                    return NotFound("Nie znaleziono pamiętników dla podanego użytkownika.");
                }

                var diaryDtos = mapper.Map<List<DiaryDto>>(diaries);
                return Ok(diaryDtos);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania pamiętników: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("{id}/Diarydays")]
        public async Task<IActionResult> GetInActiveDiaryDays(int id)
        {
            try
            {
                var days = await unitOfWork.CalendarRepository.GetAll(calendar =>
                    calendar.Diary.Id == id &&
                    calendar.IsActive == true &&
                    calendar.Id != null);

                var orderedDays = days.OrderBy(calendar => calendar.EventDate).ToList();

                return Ok(mapper.Map<List<CalendarDto>>(orderedDays));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("{id}/getDiaryPlants")]
        public async Task<IActionResult> GetInactiveDiaryPlants(int id)
        {
            try
            {

                var plants = await unitOfWork.PlantRepository.GetAll(x => x.Diary.Id == id && !x.IsActive);

                var plantReadDtos = new List<PlantReadDto>();

                foreach (var plant in plants)
                {
                    var device = await unitOfWork.DeviceRepository.Get(plant.DeviceId);
                    var plantProfile = await unitOfWork.PlantProfileRepository.Get(plant.PlantProfileId);

                    var plantReadDto = mapper.Map<PlantReadDto>(plant);
                    plantReadDto.Id = plant.Id;
                    plantReadDto.DeviceName = device?.DeviceName;
                    plantReadDto.PlantProfileName = plantProfile?.ProfileName;

                    plantReadDtos.Add(plantReadDto);
                }

                return Ok(plantReadDtos);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania roślin: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
    }
}
