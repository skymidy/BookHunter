using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        // Navigation properties
        [InverseProperty("Author")]
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}