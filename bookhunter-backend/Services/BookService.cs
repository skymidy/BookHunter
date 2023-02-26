using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookHunter_Backend.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ITagRepository _tagRepository;

        public BookService( IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository, ITagRepository tagRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _tagRepository = tagRepository;
        }
        
        public async Task AddBooks(IEnumerable<Book> books)
        {
            await _bookRepository.AddRangeAsync(books);
        }
        
        public Task AddBook(Book book)
        {
            _bookRepository.Add(book);
            return Task.CompletedTask;
        }

        public Task UpdateBook(Book book)
        {
            _bookRepository.Update(book);
            return Task.CompletedTask;
        }
        public async Task Delete(int productId)
        {
            var book = await _bookRepository.GetByIdAsync(productId);
            if (book != null)
            {
                _bookRepository.Remove(book);
            }
        }

        public async Task<IEnumerable<Book>> GetPaginated(int pageNumber, int pageSize = 10)
        {
            return await _bookRepository.GetPaginated(pageNumber, pageSize);
        }
        public async Task<IEnumerable<Book>> GetBooksByTitle(string title)
        {
            return await _bookRepository.GetBooksByTitle(title);
        }
        
        // ReSharper disable once InconsistentNaming
        public async Task<IEnumerable<Book>> GetBooksByISBN(string isbn)
        {
            return await _bookRepository.GetBooksByISBN(isbn);
        }

        public async Task<IEnumerable<Book>> GetBooksByTag(string tagName)
        {
            return await _bookRepository.GetBooksByTag(tagName);
        }
        
        public async Task<IEnumerable<Book>> GetBooksByAuthor(string authorName)
        {
            return await _bookRepository.GetBooksByAuthor(authorName);
        }
        
        public async Task<IEnumerable<Book>> GetBooksByGenre(string genreName)
        {
            return await _bookRepository.GetBooksByGenre(genreName);
        }
        public async Task AddTagToBook(int bookId, string tagName)
        {
            // Check if tag exists in database
            var tag = await _tagRepository.GetByName(tagName);
            if (tag == null)
            {
                // If tag doesn't exist, create it
                tag = new Tag{Name = tagName};
                _tagRepository.Add(tag);
            }

            // Associate tag with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookTag = new BookTag { Book = book, Tag = tag };
                book.Tags.Add(bookTag);
            }
        }
        public async Task AddAuthorToBook(int bookId, string authorName)
        {
            // Check if author exists in database
            var author = await _authorRepository.GetByName(authorName);
            if (author == null)
            {
                // If tag doesn't exist, create it
                author = new Author{Name = authorName};
                _authorRepository.Add(author);
            }

            // Associate author with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookAuthor = new BookAuthor { Book = book, Author = author };
                book.Authors.Add(bookAuthor);
            }
        }
        public async Task AddGenreToBook(int bookId, string genreName)
        {
            // Check if genre exists in database
            var genre = await _genreRepository.GetByName(genreName);
            if (genre == null)
            {
                // If tag doesn't exist, create it
                genre = new Genre{Name = genreName};
                _genreRepository.Add(genre);
            }

            // Associate genre with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookGenre = new BookGenre() { Book = book, Genre = genre };
                book.Genres.Add(bookGenre);
            }
        }
    }
}