using System;

namespace FlyyAirlines.Data
{
    public class News : BaseEntity
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublicDate { get; set; }
    }
}
