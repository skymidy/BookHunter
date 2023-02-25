using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Book> Books { get; set; }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions)
        {
            
        }
    }
}
