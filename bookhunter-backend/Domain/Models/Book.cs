using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Domain.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageId { get; set; } = string.Empty;

        // ReSharper disable once InconsistentNaming
        public string ISBN { get; set; } = string.Empty;

        public string PagesCount { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; } = DateTime.UnixEpoch;

        // Navigation properties

        [InverseProperty("Book")]
        public ICollection<BookTag> Tags { get; set; } = new List<BookTag>();

        [InverseProperty("Book")]
        public ICollection<BookAuthor> Authors { get; set; } = new List<BookAuthor>();

        [InverseProperty("Book")]
        public ICollection<BookGenre> Genres { get; set; } = new List<BookGenre>();
    }
}