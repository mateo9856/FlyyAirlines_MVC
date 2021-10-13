﻿using FlyyAirlines.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlyyAirlines.Repository
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User Get(string id);
        Task Add(User entity);
        void Update(User entity);
        Task Delete(string id);
    }
}
