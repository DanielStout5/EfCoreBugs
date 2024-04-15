namespace EfCoreOpenJson.Models
{
    public class Book
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public Dictionary<string, object>? Data { get; set; }

        public List<BookCover> Covers {get; set;} = new();

        public DateTime? PublishAt { get; set; }

        public DateTime? HideAt { get; set; }
    }
}
