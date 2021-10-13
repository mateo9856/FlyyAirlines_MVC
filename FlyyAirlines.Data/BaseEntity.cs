using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyyAirlines.Data
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
    }
}
