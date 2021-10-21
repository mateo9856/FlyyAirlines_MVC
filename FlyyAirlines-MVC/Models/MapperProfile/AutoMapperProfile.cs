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
                .ForMember(d => d.Flights, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ReservationFormModel, Reservation>()
                .ForMember(d => d.Flights, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Flight, FlightFormModel>()
                .ForMember(d => d.AirplaneId, opt => opt.MapFrom(s => s.Airplane.Id))
                .ReverseMap();

            CreateMap<FlightFormModel, Flight>()
                .ForMember(d => d.FlightName, opt => opt.MapFrom(s => (s.FromCity + " - " + s.ToCity)));
            
            CreateMap<Employee, EmployeeFormModel>().ReverseMap();
            
            CreateMap<Airplane, AirplaneFormModel>();

            CreateMap<AirplaneFormModel, Airplane>()
                .ForMember(d => d.Flights, opt => opt.Ignore()).ReverseMap();
            
            CreateMap<RegisterModel, UserFormModel>().ReverseMap();
        }
    }
}
