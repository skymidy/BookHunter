using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Domain.Models
{
    public class Genre : BaseDict
    {
        // Navigation properties
        [InverseProperty("Genre")]
        public ICollection<BookGenre> BookGenres { get; set; }

    }
}