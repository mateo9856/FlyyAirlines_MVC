namespace FlyyAirlines.Data
{
    public class Message : BaseEntity
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string AuthorId { get; set; }
        public virtual User User { get; set; }
    }
}
