using Core.Abstraction.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        public async Task<IActionResult> CreateBookAsync([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book data is required.");
            }

            try
            {
                var bookId = await _service.CreateBookAsync(book);
                return CreatedAtAction(nameof(GetBookByIdAsync), new { bookId }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookByIdAsync(Guid bookId)
        {
            var book = await _service.GetBookBuIdAsync(bookId);
            if (book == null)
            {
                return NotFound($"Book with ID {bookId} not found.");
            }

            return Ok(book);
        }
    }
}
