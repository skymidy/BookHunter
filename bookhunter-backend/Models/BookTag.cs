using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookHunter_Backend.Models
{
    public class BookTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }
        
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}