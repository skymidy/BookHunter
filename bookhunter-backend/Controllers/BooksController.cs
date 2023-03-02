using BookHunter_Backend.Domain.Models;
using BookHunter_Backend.Objects;
using BookHunter_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHunter_Backend.Controllers
{ 
        
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        
        [HttpGet("page")]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetPaginated(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1)
            {
                return BadRequest("Page number must be greater than or equal to 1.");
            }

            if (pageSize < 1 || pageSize > 100)
            {
                return BadRequest("Page size must be between 1 and 100.");
            }

            var books = await _bookService.GetPaginated(pageNumber, pageSize);

            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<BookInfo>> GetBookById(int id)
        {
            var book = await _bookService.GetBook(id);
            return Ok(book);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooksByTitle(string title)
        {
            var books = await _bookService.GetBooksByTitle(title);
            return Ok(books);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }
        [HttpGet("isbn/{isbn}")]
        // ReSharper disable once InconsistentNaming
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooksByISBN(string isbn)
        {
            var books = await _bookService.GetBooksByISBN(isbn);
            return Ok(books);
        }

        [HttpGet("tag/{tagName}")]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooksByTag(string tagName)
        {
            var books = await _bookService.GetBooksByTag(tagName);
            return Ok(books);
        }

        [HttpGet("author/{authorName}")]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooksByAuthor(string authorName)
        {
            var books = await _bookService.GetBooksByAuthor(authorName);
            return Ok(books);
        }

        [HttpGet("genre/{genreName}")]
        public async Task<ActionResult<IEnumerable<BookInfo>>> GetBooksByGenre(string genreName)
        {
            var books = await _bookService.GetBooksByGenre(genreName);
            return Ok(books);
        }
    }
}