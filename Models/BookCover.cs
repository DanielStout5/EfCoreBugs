namespace EfCoreOpenJson.Models
{
    public class BookCover : BookCover2
    {
        public int BookId { get; set; }

        public Book? Book { get; set; }
    }
}
