using BookHunter_Backend.Domain.Models;
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
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetPaginated(int pageNumber = 1, int pageSize = 10)
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

        [HttpGet("{title}")]
        public async Task<IActionResult> GetBooksByTitle(string title)
        {
            Console.WriteLine(title);
            var books = await _bookService.GetBooksByTitle(title);
            return Ok(books);
        }

        [HttpGet("isbn/{isbn}")]
        // ReSharper disable once InconsistentNaming
        public async Task<IActionResult> GetBooksByISBN(string isbn)
        {
            var books = await _bookService.GetBooksByISBN(isbn);
            return Ok(books);
        }

        [HttpGet("tag/{tagName}")]
        public async Task<IActionResult> GetBooksByTag(string tagName)
        {
            var books = await _bookService.GetBooksByTag(tagName);
            return Ok(books);
        }

        [HttpGet("author/{authorName}")]
        public async Task<IActionResult> GetBooksByAuthor(string authorName)
        {
            var books = await _bookService.GetBooksByAuthor(authorName);
            return Ok(books);
        }

        [HttpGet("genre/{genreName}")]
        public async Task<IActionResult> GetBooksByGenre(string genreName)
        {
            var books = await _bookService.GetBooksByGenre(genreName);
            return Ok(books);
        }
    }
}