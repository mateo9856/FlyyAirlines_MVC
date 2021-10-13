using System.ComponentModel.DataAnnotations.Schema;

namespace FlyyAirlines.Data
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string WorkPosition { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
