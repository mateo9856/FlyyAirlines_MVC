using FlyyAirlines.Data;
using FlyyAirlines.Repository;
using FlyyAirlines.Services.Account;
using FlyyAirlines.Services.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseService<Reservation>, BaseService<Reservation>>();
            services.AddScoped<IBaseService<Flight>, BaseService<Flight>>();
            services.AddScoped<IBaseService<Airplane>, BaseService<Airplane>>();
            services.AddScoped<IBaseService<Employee>, BaseService<Employee>>();
            services.AddScoped<IBaseService<News>, BaseService<News>>();
            services.AddScoped<IBaseService<Message>, BaseService<Message>>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IReserveService, ReserveService>();
            services.AddScoped<IAirplanesFlightsService, AirplanesFlightsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
