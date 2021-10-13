using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FlyyAirlines.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
            Messages = new HashSet<Message>();
        }
        public override string Id { get; set; }
        public override string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public override string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
