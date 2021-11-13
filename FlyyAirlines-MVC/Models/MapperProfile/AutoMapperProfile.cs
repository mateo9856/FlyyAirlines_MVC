using AutoMapper;
using FlyyAirlines.Data;
using FlyyAirlines_MVC.Models.EmployeeActions;
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
            UserMapper();
            ReservationMapper();
            FlightAirplaneMapper();
            NewsMapper();
            PermissionMapper();
            EmployeeMapper();
        }

        private void UserMapper()
        {
            CreateMap<User, UserFormModel>().ReverseMap();

            CreateMap<RegisterModel, UserFormModel>().ReverseMap();
        }

        private void ReservationMapper()
        {
            CreateMap<Reservation, ReservationFormModel>()
                .ForMember(d => d.FlightId, opt => opt.MapFrom(s => s.Flights.Id))
                .ForMember(d => d.FlightsList, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Reservation, ReservationSummaryModel>()
                .ForMember(d => d.FlightName, opt => opt.MapFrom(d => d.Flights.FlightName))
                .ReverseMap();


            CreateMap<ReservationFormModel, Reservation>()
                .ForMember(d => d.Flights, opt => opt.Ignore())
                .ForMember(d => d.User, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Reservation, CheckReserveModel>()
                .ForMember(d => d.ReservationId, opt => opt.MapFrom(d => d.Id))
                .ForMember(d => d.FlightName, opt => opt.MapFrom(d => d.Flights.FlightName))
                .ForMember(d => d.AirplaneName, opt => opt.MapFrom(d => d.Flights.Airplane.PlaneName))
                .ForMember(d => d.IsChecked, opt => opt.Ignore())
                .ReverseMap();
        }

        private void FlightAirplaneMapper()
        {
            CreateMap<Airplane, AirplaneFormModel>();

            CreateMap<AirplaneFormModel, Airplane>()
                .ForMember(d => d.Flights, opt => opt.Ignore()).ReverseMap();

            CreateMap<Flight, FlightFormModel>()
                .ForMember(d => d.AirplaneId, opt => opt.MapFrom(s => s.Airplane.Id))
                .ReverseMap();

            CreateMap<FlightFormModel, Flight>()
                .ForMember(d => d.FlightName, opt => opt.MapFrom(s => (s.FromCity + " - " + s.ToCity)));
        }

        private void NewsMapper()
        {
            CreateMap<News, NewsFormModel>()
                .ForMember(d => d.Image, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<NewsFormModel, News>()
                .ForMember(d => d.ImageUrl, opt => opt.Ignore());
        }

        private void PermissionMapper()
        {
            CreateMap<Permission, PermissionFormModel>()
                .ReverseMap();
        }

        private void EmployeeMapper()
        {
            CreateMap<Employee, EmployeeFormModel>()
                .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(d => d.Email, opt => opt.MapFrom(d => d.User.Email))
                .ForMember(d => d.EmployeeOperation, opt => opt.Ignore())
                .ForMember(d => d.EmployeePermissions, opt => opt.MapFrom(d => d.User.Permissions.ToList()));

        }

    }
}
