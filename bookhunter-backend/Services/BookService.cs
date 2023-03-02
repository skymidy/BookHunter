using BookHunter_Backend.Domain.Interfaces;
using BookHunter_Backend.Domain.Models;
using BookHunter_Backend.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookHunter_Backend.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ITagRepository _tagRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,
        IGenreRepository genreRepository, ITagRepository tagRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _tagRepository = tagRepository;
        }

        private async Task<BookInfo> BookBuilder(Book bookModel)
        {
            var book = BookInfo.Success(bookModel);
            book.SetAuthors(await _bookRepository.GetBookAuthors(bookModel.Id));
            book.SetGenres(await _bookRepository.GetBookGenres(bookModel.Id));
            book.SetTags(await _bookRepository.GetBookTags(bookModel.Id));
            return book;
        }

        private async Task<IEnumerable<BookInfo>> BookListBuilder(IEnumerable<Book> booksModels)
        {
            var books = new List<BookInfo>();
            foreach (var bookModel in booksModels)
            {
                books.Add(await BookBuilder(bookModel));
            }

            return books;
        }

        public async Task AddBooks(IEnumerable<Book> books)
        {
            await _bookRepository.AddRangeAsync(books);
        }

        public async Task AddBooks(IEnumerable<BookInput> books)
        {
            await _bookRepository.AddRangeAsync(books.Select(
            bookInput =>
            {
                var bookModel = new Book();
                bookInput.CopyToModel(bookModel);
                return bookModel;
            }).ToList());
        }

        public Book AddBook(Book book)
        {
            return _bookRepository.Add(book);
        }

        public Book AddBook(BookInput book)
        {
            var bookModel = new Book();
            book.CopyToModel(bookModel);
            return _bookRepository.Add(bookModel);
        }

        public Task UpdateBook(Book book)
        {
            _bookRepository.Update(book);
            return Task.CompletedTask;
        }

        public async void UpdateBook(int id, BookInput book)
        {
            var bookModel  = await _bookRepository.GetByIdAsync(id);
            if (bookModel == null) throw new Exception("Error: Invalid Book Id");
            book.CopyToModel(bookModel);
            _bookRepository.Update(bookModel);
        }

        public async Task Delete(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                _bookRepository.Remove(book);
            }
        }

        public async Task<BookInfo?> GetBook(int id)
        {
            var bookModel = await _bookRepository.GetByIdAsync(id);
            if (bookModel == null) throw new Exception("Error: Invalid Book id");
            return await BookBuilder(bookModel);
        }

        public async Task<IEnumerable<BookInfo>> GetAllAsync()
        {
            return await BookListBuilder(await _bookRepository.GetAllAsync());
        }
        public async Task<IEnumerable<Book>> GetAllModelsAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<IEnumerable<BookInfo>> GetPaginated(int pageNumber, int pageSize = 10)
        {
            return await BookListBuilder(await _bookRepository.GetPaginated(pageNumber, pageSize));
        }

        public async Task<IEnumerable<BookInfo>> GetBooksByTitle(string title)
        {
            return await BookListBuilder(await _bookRepository.GetBooksByTitle(title));
        }

        // ReSharper disable once InconsistentNaming
        public async Task<IEnumerable<BookInfo>> GetBooksByISBN(string isbn)
        {
            return await BookListBuilder(await _bookRepository.GetBooksByISBN(isbn));
        }

        public async Task<IEnumerable<BookInfo>> GetBooksByTag(string tagName)
        {
            return await BookListBuilder(await _bookRepository.GetBooksByTag(tagName));
        }

        public async Task<IEnumerable<BookInfo>> GetBooksByAuthor(string authorName)
        {
            return await BookListBuilder(await _bookRepository.GetBooksByAuthor(authorName));
        }

        public async Task<IEnumerable<BookInfo>> GetBooksByGenre(string genreName)
        {
            return await BookListBuilder(await _bookRepository.GetBooksByGenre(genreName));
        }

        public async Task AddTagToBook(int bookId, string tagName)
        {
            // Check if tag exists in database
            var tag = _tagRepository.GetByName(tagName);
            if (tag == null)
            {
                // If tag doesn't exist, create it
                tag = new Tag {Name = tagName};
                _tagRepository.Add(tag);
            }

            // Associate tag with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookTag = new BookTag {Book = book, Tag = tag};
                book.Tags.Add(bookTag);
            }
        }

        public async Task AddAuthorToBook(int bookId, string authorName)
        {
            // Check if author exists in database
            var author = _authorRepository.GetByName(authorName);
            if (author == null)
            {
                // If tag doesn't exist, create it
                author = new Author {Name = authorName};
                _authorRepository.Add(author);
            }

            // Associate author with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookAuthor = new BookAuthor {Book = book, Author = author};
                book.Authors.Add(bookAuthor);
            }
        }

        public async Task AddGenreToBook(int bookId, string genreName)
        {
            // Check if genre exists in database
            var genre = _genreRepository.GetByName(genreName);
            if (genre == null)
            {
                // If tag doesn't exist, create it
                genre = new Genre {Name = genreName};
                _genreRepository.Add(genre);
            }

            // Associate genre with book
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var bookGenre = new BookGenre() {Book = book, Genre = genre};
                book.Genres.Add(bookGenre);
            }
        }
    }
}