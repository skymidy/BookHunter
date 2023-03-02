using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Objects
{
    public class BookInput
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageId { get; set; } = string.Empty;

        // ReSharper disable once InconsistentNaming
        public string ISBN { get; set; } = string.Empty;

        public string PagesCount { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } = DateTime.UnixEpoch;

        // Navigation properties
        public ICollection<DictInfo> Tags { get; set; } = new List<DictInfo>();

        public ICollection<DictInfo> Authors { get; set; } = new List<DictInfo>();

        public ICollection<DictInfo> Genres { get; set; } = new List<DictInfo>();

        public string Message { get; set; } = "ok";

        public void CopyToModel(Book book)
        {
            book.Title = Title;
            book.Description = Description;
            book.ImageId = ImageId;
            book.ISBN = ISBN;
            book.PagesCount = PagesCount;
            book.PublishedDate = PublishedDate;

        }
    }
}