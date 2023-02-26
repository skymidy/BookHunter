using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        public BookRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetBooksByTitle(string title)
        {
            return await Set
            .Where(b => b.Title.ToLower().Contains(title.ToLower()))
            .ToListAsync();
        }
        
        // ReSharper disable once InconsistentNaming
        public async Task<IEnumerable<Book>> GetBooksByISBN(string isbn)
        {
            return await Set
            .Where(b => b.ISBN == isbn)
            .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByTag(string tagName)
        {
            tagName = tagName.ToLower();
            return await Set
                .Where(b => b.Tags.Any(bt => bt.Tag.Name == tagName))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Book>> GetBooksByAuthor(string authorName)
        {
            authorName = authorName.ToLower();
            return await Set
                .Where(b => b.Authors.Any(bt => bt.Author.Name == authorName))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Book>> GetBooksByGenre(string genreName)
        {
            genreName = genreName.ToLower();
            return await Set
                .Where(b => b.Genres.Any(bt => bt.Genre.Name == genreName))
                .ToListAsync();
        }
    }
}