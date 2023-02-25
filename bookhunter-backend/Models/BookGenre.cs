using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Models
{
    public class BookGenre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }
        
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}