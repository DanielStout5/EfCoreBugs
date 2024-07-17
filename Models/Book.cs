using System.ComponentModel.DataAnnotations.Schema;
using static EfCoreOpenJson.Models.IBookCover2Haver;

namespace EfCoreOpenJson.Models
{
    public class Book : IBookCover2Haver
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public Dictionary<string, object>? Data { get; set; }

        public List<BookCover> Covers { get; set; } = new();

        public DateTime? PublishAt { get; set; }

        public DateTime? HideAt { get; set; }

        [NotMapped]
        List<BookCover2> IBookCover2Haver.Covers => Covers.Cast<BookCover2>().ToList();
    }

    public interface IBookCover2Haver
    {
        List<BookCover2> Covers { get; }
    }

    public abstract class BookCover2
    {
        public int Id { get; set; }
    }
}
