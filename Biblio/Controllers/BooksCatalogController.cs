using AutoMapper;
using Biblio.DAL.Interfaces;
using Biblio.DAL.Models.Book;
using Biblio.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Controllers
{
    [ApiController]
    [Route("/books_catalog")]
    public class BooksCatalogController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BooksCatalogController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Book> _validator;

        public BooksCatalogController(IBookRepository repository,
            ILogger<BooksCatalogController> logger,
            IMapper mapper, 
            IValidator<Book> validator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("add_book")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var result = await _validator.ValidateAsync(book).ConfigureAwait(true);

            if (result.IsValid)
            {
                await _repository.AddAsync(book);
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }

            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            return BadRequest(errorsMessages);
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var collection = (await _repository.GetAllAsync().ConfigureAwait(true))
                .Select(b => _mapper.Map<BookDto>(b));
            return Ok(collection);
        }

        [HttpGet("book/{id:length(24)}")]
        public async Task<IActionResult> GetBookById([FromRoute] string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if (book is null) return NotFound();

            var bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }

        [HttpPut("edit_book/{id:length(24)}")]
        public async Task<IActionResult> EditBook([FromBody] Book updateBook, string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if(book is null) return NotFound();

            var result = await _validator.ValidateAsync(updateBook);

            if (result.IsValid)
            {
                updateBook.Id = book.Id;

                await _repository.UpdateAsync(updateBook, id);

                return NoContent();
            }

            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            return BadRequest(errorsMessages);
        }

        [HttpDelete("delete_book/{id:length(24)}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string id)
        {
            var book = await _repository.GetEntityAsync(id).ConfigureAwait(true);

            if (book is null) return NotFound();

            await _repository.DeleteAsync(book.Id);

            return NoContent();
        }
    }
}