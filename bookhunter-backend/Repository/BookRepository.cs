using BookHunter_Backend.Domain;
using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.OpenApi.Any;

namespace BookHunter_Backend.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await Set.AsSingleQuery().ToListAsync();
        }

        public new async Task<Book> GetByIdAsync(int id)
        {
            var result = await Set
            .Include(b=>b.Authors).ThenInclude(ba=> ba.Author)
            .Include(b=>b.Tags).ThenInclude(bt=> bt.Tag)
            .Include(b=>b.Genres).ThenInclude(bg=> bg.Genre)
            .Where(b => b.Id == id).FirstAsync();
            return result;
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
        
        public async Task<IEnumerable<Tag>> GetBookTags(int id)
        {
            var book = await GetByIdAsync(id);
            if (book == null) throw new Exception("Error: Invalid Book Id!");
            return book.Tags.Select(bt => bt.Tag).ToList();
        }
        public async Task<IEnumerable<Author>> GetBookAuthors(int id)
        {
            var book = await GetByIdAsync(id);
            if (book == null) throw new Exception("Error: Invalid Book Id!");
            return book.Authors.Select(ba => ba.Author).ToList();
        }
        public async Task<IEnumerable<Genre>> GetBookGenres(int id)
        {
            var book = await GetByIdAsync(id);
            if (book == null) throw new Exception("Error: Invalid Book Id!");
            return book.Genres.Select(bg => bg.Genre).ToList();
        }
    }
}