using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;
using GardenApp.API.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.API.Controllers
{
    [GardenAppV1Route]
    [ApiController]
    public class PlantController : ControllerBase
    {
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
    }
}
