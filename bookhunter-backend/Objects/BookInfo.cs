using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Objects
{
    public class BookInfo
    {
        public int Id { get; set; }

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

        public static BookInfo Success(Book model)
        {
            return new BookInfo()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                ImageId = model.ImageId,
                ISBN = model.ISBN,
                PagesCount = model.PagesCount,
                PublishedDate = model.PublishedDate
            };
        }

        public static SiteParserInfo Fail(string message)
        {
            return new SiteParserInfo()
            {
                Message = message
            };
        }

        public void SetTags(IEnumerable<Tag> tags)
        {
            Tags = tags.Select(t=> DictInfo.Success(t.Id,t.Name)).ToList();
        }

        public void SetAuthors(IEnumerable<Author> authors)
        {
            Authors = authors.Select(t=> DictInfo.Success(t.Id,t.Name)).ToList();
        }

        public void SetGenres(IEnumerable<Genre> genres)
        {
            Genres = genres.Select(t=> DictInfo.Success(t.Id,t.Name)).ToList();
        }
    }
}