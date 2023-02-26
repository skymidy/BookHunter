using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Domain.Models
{
    public class Author : BaseDict
    {
        // Navigation properties
        [InverseProperty("Author")]
        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}