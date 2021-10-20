using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines_MVC.Models.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyyAirlines_MVC.Models.MapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserFormModel>().ReverseMap();
            
            CreateMap<Reservation, ReservationFormModel>()
                .ForMember(d => d.FlightId, opt => opt.MapFrom(s => s.Flights.Id))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User.Id))
                .ReverseMap();
            
            CreateMap<Flight, FlightFormModel>()
                .ForMember(d => d.AirplaneId, opt => opt.MapFrom(s => s.Airplane.Id))
                .ReverseMap();
            
            CreateMap<Employee, EmployeeFormModel>().ReverseMap();
            
            CreateMap<Airplane, AirplaneFormModel>().ReverseMap();

            CreateMap<AirplaneFormModel, Airplane>();
            
            CreateMap<RegisterModel, UserFormModel>().ReverseMap();
        }
    }
}
