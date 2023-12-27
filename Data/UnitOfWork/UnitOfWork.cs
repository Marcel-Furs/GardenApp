using GardenApp.API.Data.Models;
using GardenApp.API.Data.Repositories;

namespace GardenApp.API.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
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

        public UnitOfWork(DataContext context)
        {
            CalendarRepository = new BaseCrudRepository<Calendar, int>(context);
            ConditionRepository = new BaseCrudRepository<Condition, int>(context);
            DeviceRepository = new BaseCrudRepository<Device, int>(context);
            HistoryRepository = new BaseCrudRepository<History, int>(context);
            NotificationRepository = new BaseCrudRepository<Notification, int>(context);
            PlantRepository = new BaseCrudRepository<Plant, int>(context);
            PlantProfileRepository = new BaseCrudRepository<PlantProfile, int>(context);
            ProjectTaskRepository = new BaseCrudRepository<ProjectTask, int>(context);
            UserRepository = new BaseCrudRepository<User, int>(context);
            WeatherMeasurementRepository = new BaseCrudRepository<WeatherMeasurement, int>(context);
        }
    }
}
