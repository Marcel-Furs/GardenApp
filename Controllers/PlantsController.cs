using AutoMapper;
using GardenApp.API.Attributes;
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

        [HttpGet("getPlants")]
        public async Task<IActionResult> GetPlants()
        {
            try
            {
                var plants = await unitOfWork.PlantRepository.GetAll();
                var plantDtos = mapper.Map<List<PlantCreateDto>>(plants);


                return Ok(plantDtos);
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
            //string result = name.Replace("\\", "/");
            var path = Path.Combine(MediaDir, name);
            if (System.IO.File.Exists(path))
            {
                Byte[] b = System.IO.File.ReadAllBytes(path);
                return File(b, "image/" + Path.GetExtension(path));
            }

            return NotFound();
        }
    }
}
