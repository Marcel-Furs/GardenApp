using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;

namespace GardenApp.API.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Calendar, int> CalendarRepository { get; set; }
        public IBaseRepository<Condition, int> ConditionRepository { get; set; }
        public IBaseRepository<Device, int> DeviceRepository { get; set; }
        public IBaseRepository<History, int> HistoryRepository { get; set; }
        public IBaseRepository<Notification, int> NotificationRepository { get; set; }
        public IBaseRepository<Plant, int> PlantRepository { get; set; }
        public IBaseRepository<PlantProfile, int> PlantProfileRepository { get; set; }
        public IBaseRepository<ProjectTask, int> ProjectTaskRepository { get; set; }
        public IBaseRepository<User, int> UserRepository { get; set; }
        public IBaseRepository<WeatherMeasurement, int> WeatherMeasurementRepository { get; set; }
        public IBaseRepository<Sensor, int> SensorRepository { get; set; }
        public IBaseRepository<SensorType, int> SensorTypeRepository { get; set; }
    }
}
