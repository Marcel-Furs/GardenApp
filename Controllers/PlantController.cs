using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;
using GardenApp.API.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private static readonly string MediaDir = "./media";
        private readonly IUnitOfWork unitOfWork;

        public PlantController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("create")]
        public async Task<IActionResult> TestCreate([FromBody] string name)
        {
            await unitOfWork.PlantRepository.Create(new Plant
            {
                PlantName = name
            });
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //return Ok(await plantRepository.GetAll());
            // return Ok(await plantRepository.Get(1));
            //return Ok(await unitOfWork.PlantRepository.GetAll(x => x.Id == 2));
            return Ok(await unitOfWork.PlantRepository.Get(x => x.PlantName == "stokrotka"));
        }

        [HttpGet("Marcel")]
        public async Task<IActionResult> GetId()
        {
            //return Ok(await plantRepository.GetAll());
            // return Ok(await plantRepository.Get(1));
            return Ok(await unitOfWork.PlantRepository.GetAll(x => x.Id == 2));
            //return Ok(await unitOfWork.PlantRepository.Get(x => x.PlantName == "stokrotka"));
        }

        [HttpPost("{plantId}/ImportFile")]
        public async Task<IActionResult> ImportFile(int plantId, [FromForm] IFormFile file)
        {
            //przypisanie planta do zmiennej na podstawie plantId
            //jesli nie istnieje to zwroc jakis 404

            string name = file.FileName;
            string extension = Path.GetExtension(file.FileName);
            //read the file
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                if(memoryStream.Length > 0)
                {
                    if(!Directory.Exists(MediaDir))
                    {
                        Directory.CreateDirectory(MediaDir);
                    }
                    byte[] fileBytes = memoryStream.ToArray();
                    System.IO.File.WriteAllBytes(Path.Combine(MediaDir, Guid.NewGuid() + "_" + name), fileBytes);
                    //plant.ImagePath = Path.Combine(MediaDir, Guid.NewGuid() + "_" + name)
                    //service.updatePlant(plant)
                }
            }

            return Ok();
        }

        [HttpGet("Image/{name}")]
        public IActionResult GetImage(string name)
        {
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
