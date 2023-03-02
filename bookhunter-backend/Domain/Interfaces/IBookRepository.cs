using BookHunter_Backend.Domain.Models;

namespace BookHunter_Backend.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByTitle(string title);
        
        // ReSharper disable once InconsistentNaming
        Task<IEnumerable<Book>> GetBooksByISBN(string isbn);

        Task<IEnumerable<Book>> GetBooksByTag(string tagName);

        Task<IEnumerable<Book>> GetBooksByAuthor(string authorName);

        Task<IEnumerable<Book>> GetBooksByGenre(string genreName);
        Task<IEnumerable<Author>> GetBookAuthors(int id);
        Task<IEnumerable<Tag>> GetBookTags(int id);
        Task<IEnumerable<Genre>> GetBookGenres(int id);
    }
}