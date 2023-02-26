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
    }
}