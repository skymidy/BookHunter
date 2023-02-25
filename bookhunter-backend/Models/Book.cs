using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageId { get; set; }

        public string ISBN { get; set; }

        public int PagesCount { get; set; }

        public DateTime PublishedDate { get; set; }

        // Navigation properties
        
        [InverseProperty("Book")]
        public ICollection<BookTag> Tags { get; set; }

        [InverseProperty("Book")]
        public ICollection<BookAuthor> Authors { get; set; }

        [InverseProperty("Book")]
        public ICollection<BookGenre> Genres { get; set; }
    }
}