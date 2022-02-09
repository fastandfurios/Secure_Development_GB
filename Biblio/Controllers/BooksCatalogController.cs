using Biblio.DAL.Interfaces;
using Biblio.DAL.Models.Book;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Controllers
{
    [ApiController]
    [Route("/books_catalog")]
    public class BooksCatalogController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BooksCatalogController> _logger;

        public BooksCatalogController(IBookRepository repository, ILogger<BooksCatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("add_book")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            await _repository.AddAsync(book).ConfigureAwait(true);
            return Ok();
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var collection = await _repository.GetAllAsync().ConfigureAwait(true);
            return Ok(collection);
        }

        [HttpGet("book/{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if (book is null) return NotFound();

            return Ok(book);
        }

        [HttpPut("edit_book/{id}")]
        public async Task<IActionResult> EditBook([FromBody] Book updateBook, string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if(book is null) return NotFound();

            updateBook.Id = book.Id;

            await _repository.UpdateAsync(updateBook, id);

            return Ok();
        }

        [HttpDelete("delete_book/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if (book is null) return NotFound();

            await _repository.DeleteAsync(book.Id);

            return NoContent();
        }
    }
}