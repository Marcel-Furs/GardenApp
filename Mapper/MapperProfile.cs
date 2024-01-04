using AutoMapper;
using GardenApp.API.Data.Models;
using GardenApp.API.Dto;

namespace GardenApp.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Calendar, CalendarDto>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.Id))
                .ForMember(x => x.Description, y => y.MapFrom(x => x.Description))
                .ForMember(x => x.EventDate, y => y.MapFrom(x => x.EventDate))
                .ForMember(x => x.IsActive, y => y.MapFrom(x => x.IsActive))
                .ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId));

            CreateMap<CalendarDto, Calendar>()
                .ForMember(x => x.Description, y => y.MapFrom(x => x.Description))
                .ForMember(x => x.EventDate, y => y.MapFrom(x => x.EventDate))
                .ForMember(x => x.IsActive, y => y.MapFrom(x => x.IsActive))
                .ForMember(x => x.UserId, y => y.MapFrom(x => x.UserId))
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.User, y => y.Ignore());

            CreateMap<WeatherMeasurement, WeatherMeasurementDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.MeasurementTime, opt => opt.MapFrom(src => src.MeasurementTime))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Precipitation, opt => opt.MapFrom(src => src.Precipitation));

            CreateMap<WeatherMeasurementDto, WeatherMeasurement>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.MeasurementTime, opt => opt.MapFrom(src => src.MeasurementTime))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Precipitation, opt => opt.MapFrom(src => src.Precipitation))
                // Ignoruj pola, które nie są bezpośrednio mapowane
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<SensorType, SensorTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit));

            CreateMap<SensorTypeDto, SensorType>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.Sensors, opt => opt.MapFrom(src => src.Sensors))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<DeviceDto, Device>()
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.Sensors, opt => opt.MapFrom(src => src.Sensors))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<PlantProfile, PlantProfileDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProfileName, opt => opt.MapFrom(src => src.ProfileName));

            CreateMap<PlantProfileDto, PlantProfile>()
                .ForMember(dest => dest.ProfileName, opt => opt.MapFrom(src => src.ProfileName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Plants, opt => opt.Ignore());

            CreateMap<Condition, ConditionDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.PlantId));


            CreateMap<ConditionDto, Condition>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.PlantId, opt => opt.MapFrom(src => src.PlantId))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Plant, opt => opt.Ignore());


            CreateMap<PlantCreateDto, Plant>()
               .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.PlantName))
               .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
               .ForMember(dest => dest.PlantProfileId, opt => opt.MapFrom(src => src.PlantProfileId))
               // PathImage będzie ustawione w logice biznesowej, ponieważ wymaga przetworzenia IFormFile
               .ForMember(dest => dest.PathImage, opt => opt.Ignore())
               // Ignoruj właściwości nawigacyjne i/lub inne właściwości, które nie powinny być mapowane
               .ForMember(dest => dest.Device, opt => opt.Ignore())
               .ForMember(dest => dest.PlantProfile, opt => opt.Ignore());

            CreateMap<Plant, PlantCreateDto>()
            .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.PlantName))
            .ForMember(dest => dest.PathImage, opt => opt.MapFrom(src => src.PathImage))
            .ForMember(dest => dest.DeviceId, opt => opt.MapFrom(src => src.DeviceId))
            .ForMember(dest => dest.PlantProfileId, opt => opt.MapFrom(src => src.PlantProfileId))
            // Ignoruj Image, ponieważ jest to IFormFile i nie możemy mapować z stringa na plik
            .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Plant, PlantReadDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.PlantName, opt => opt.MapFrom(src => src.PlantName))
           .ForMember(dest => dest.PathImage, opt => opt.MapFrom(src => src.PathImage))
           // Mapowanie nazw urządzeń i profili roślin
           .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Device.DeviceName)) // Zakładając, że Plant ma powiązanie z Device
           .ForMember(dest => dest.PlantProfileName, opt => opt.MapFrom(src => src.PlantProfile.ProfileName)); // Zakładając, że Plant ma powiązanie z PlantProfile

            CreateMap<Diary, DiaryDto>()
             .ForMember(dto => dto.PlantIds, opt => opt.MapFrom(diary => diary.Plants.Select(p => p.Id)))
             .ForMember(dto => dto.CalendarIds, opt => opt.MapFrom(diary => diary.Calendars.Select(c => c.Id)));

            // Mapowanie DiaryDto -> Diary
            CreateMap<DiaryDto, Diary>()
                .ForMember(diary => diary.Plants, opt => opt.Ignore()) // Ignore, ponieważ wymaga specjalnej obsługi
                .ForMember(diary => diary.Calendars, opt => opt.Ignore()) // Ignore, podobnie jak wyżej
                .ForMember(diary => diary.User, opt => opt.MapFrom(dto => new User { Id = dto.UserId })); // Zakładamy prostą konwersję tylko z UserId
        }
    }
}
