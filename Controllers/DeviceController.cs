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
    public class DeviceController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DeviceController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("{id}/register-device")]
        public async Task<IActionResult> RegisterDevice([FromBody] DeviceDto deviceDto)
        {
            var device = new Device
            {
                DeviceName = deviceDto.DeviceName,
                Sensors = new List<Sensor>(),
                UserId = deviceDto.UserId
            };

            foreach (var sensorDto in deviceDto.Sensors)
            {
                var sensorType = await unitOfWork.SensorTypeRepository.Get(sensorDto.SensorTypeId);
                if (sensorType == null)
                {
                    return NotFound($"SensorType o ID {sensorDto.SensorTypeId} nie został znaleziony.");
                }

                var sensor = new Sensor
                {
                    SensorType = sensorType,
                    SensorValue = sensorDto.SensorValue,
                    Device = device 
                };
                device.Sensors.Add(sensor);
            }

            await unitOfWork.DeviceRepository.Add(device);

            return Ok(new { message = "Urządzenie zarejestrowane pomyślnie." });
        }
        [HttpPost("createSensorType")]
        public async Task<IActionResult> CreateSensor([FromBody] SensorTypeDto sensorTypeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await unitOfWork.SensorTypeRepository.Create(new SensorType
                {
                    Name = sensorTypeDto.Name,
                    MeasurementUnit = sensorTypeDto.MeasurementUnit,
                });
                return Ok(new { Message = "Created" });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd przy tworzeniu sensortype: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
        [HttpGet("getSensorType")]
        public async Task<IActionResult> GetSensorType()
        {
            try
            {
                var sensorTypes = await unitOfWork.SensorTypeRepository.GetAll();

                return Ok(mapper.Map<List<SensorTypeDto>>(sensorTypes));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Błąd podczas pobierania dni kalendarza: {ex.Message}");
                return StatusCode(500, "Wystąpił błąd podczas przetwarzania żądania.");
            }
        }
    }
}
