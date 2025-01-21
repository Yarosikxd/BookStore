using Core.Abstraction.Repository;
using Core.Abstraction.Services;
using Core.Models;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
           return await _repository.CreateBookAsync(book);
        }

        public async Task<Guid?> DeleteBookAsync(Guid bookId)
        {
            return await _repository.DeleteBookAsync(bookId);
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _repository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookBuIdAsync(Guid bookId)
        {
           return await GetBookBuIdAsync(bookId);
        }

        public async Task<Guid> UpdateBookAsync(Guid bookId, string author, int year, string description)
        {
            return await _repository.UpdateBookAsync(bookId, author, year, description);
        }
    }
}
