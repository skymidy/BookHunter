using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Domain.Models
{
    public class Tag : BaseDict
    {
        // Navigation properties
        [InverseProperty("Tag")]
        public ICollection<BookTag> BookTags { get; set; }

        
    }
}