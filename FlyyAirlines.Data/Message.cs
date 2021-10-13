namespace FlyyAirlines.Data
{
    public class Message : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public User User { get; set; }
    }
}
