using Api.Contracts.Books;
using Core.Abstraction.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            try
            {
                var books = await _service.GetAllBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookRequest request )
        {
            try 
            {
                var book = Book.Create(
                    Guid.NewGuid(),
                    request.Title,
                    request.Author,
                    request.Year,
                    request.Description);

                var bookId = await _service.CreateBookAsync(book);
                return Ok(bookId);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateBook {id}")]
        public async Task<IActionResult> UpdateBookAsync(Guid id, UpdateBookRequest request)
        {
            try 
            {
                await _service.UpdateBookAsync( id, request.Title, request.Author, request.Year, request.Description);
                return Ok();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteBookAsync(Guid id)
        {
            try
            {
                await _service.DeleteBookAsync(id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);   
            }
        }

    }
}
