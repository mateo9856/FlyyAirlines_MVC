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
            CreateMap<Reservation, ReservationFormModel>().ReverseMap();
            CreateMap<Flight, FlightFormModel>().ReverseMap();
            CreateMap<Employee, EmployeeFormModel>().ReverseMap();
            CreateMap<Airplane, AirplaneFormModel>().ReverseMap();
            CreateMap<RegisterModel, UserFormModel>().ReverseMap();
        }
    }
}
