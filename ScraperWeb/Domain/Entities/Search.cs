namespace ScraperWeb.Domain.Entities
{
    public class Search
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int Occurrences { get; set; }
    }

    
}
