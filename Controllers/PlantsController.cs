using AutoMapper;
using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.UnitOfWork;
using GardenApp.API.Dto;
using GardenApp.API.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class PlantsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private static readonly string MediaDir = "./media";
        public PlantsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("{id}/getPlants")]
        public async Task<IActionResult> GetPlants(int id)
        {
            try
            {
                // Pobieranie tylko profili roślin i urządzeń należących do użytkownika o danym ID
                var devices = await unitOfWork.DeviceRepository.GetAll(x => x.UserId == id);
                var plantProfiles = await unitOfWork.PlantProfileRepository.GetAll();

                var deviceIds = devices.Select(d => d.Id).ToList(); // Pobierz Id urządzeń do listy
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
    }
}
