namespace EfCoreOpenJson.Models
{
    public class BookCover
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}
