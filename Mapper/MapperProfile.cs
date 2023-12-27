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
        }
    }
}
