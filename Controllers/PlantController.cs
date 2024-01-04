using AutoMapper;
using GardenApp.API.Attributes;
using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;
using GardenApp.API.Data.UnitOfWork;
using GardenApp.API.Dto;
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
        private readonly IMapper mapper;

        public PlantController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> ImportFile([FromForm] PlantCreateDto model)
        {
            //var deviceExists = await unitOfWork.DeviceRepository.Exists(device => device.Id == model.DeviceId);
            //var profileExists = await unitOfWork.PlantProfileRepository.Exists(profile => profile.Id == model.PlantProfileId);
            //if (!deviceExists || !profileExists)
            //{
            //    return NotFound("Device or PlantProfile not found");
            //}

            var file = model.Image;
            string name = file.FileName;
            string extension = Path.GetExtension(file.FileName);
            //read the file
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);

                if (memoryStream.Length > 0)
                {
                    if (!Directory.Exists(MediaDir))
                    {
                        Directory.CreateDirectory(MediaDir);
                    }
                    byte[] fileBytes = memoryStream.ToArray();
                    var path = Path.Combine(MediaDir, Guid.NewGuid() + "_" + name);
                    System.IO.File.WriteAllBytes(path, fileBytes);
                    Plant plant = new Plant
                    {
                        PathImage = path,
                        PlantName = model.PlantName,
                        DeviceId = model.DeviceId,
                        PlantProfileId = model.PlantProfileId
                    };
                    await unitOfWork.PlantRepository.Add(plant);
                    return Ok(new { Message = "Created" });
                }
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}/getDevice")]
        public async Task<IActionResult> GetDevice(int id)
        {
            try
            {
                var devices = await unitOfWork.DeviceRepository.GetAll(x => x.UserId == id);

                return Ok(mapper.Map<List<DeviceDto>>(devices));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania urządzeń: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("getPlantProfiles")]
        public async Task<IActionResult> GetPlantProfile()
        {
            try
            {
                var plantProfiles = await unitOfWork.PlantProfileRepository.GetAll();

                return Ok(mapper.Map<List<PlantProfileDto>>(plantProfiles));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania profilukwiatkow: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpPost("createPlantProfile")]
        public async Task<IActionResult> CreateSensor([FromBody] PlantProfileDto plantProfileDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await unitOfWork.PlantProfileRepository.Create(new PlantProfile
                {
                    ProfileName = plantProfileDto.ProfileName
                });
                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu rodzaju rośliny: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }

        [HttpPost("createCondition")]
        public async Task<IActionResult> CreateCondition([FromBody] ConditionDto conditionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await unitOfWork.ConditionRepository.Create(new Condition
                {
                    Description = conditionDto.Description
                });
                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu rodzaju rośliny: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
    }
}
