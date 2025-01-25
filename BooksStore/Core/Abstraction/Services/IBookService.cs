using Core.Models;

namespace Core.Abstraction.Services
{
    public interface IBookService
    {
        Task<Guid> CreateBookAsync(Book book);
        Task<Book> GetBookBuIdAsync(Guid bookId);
        Task<List<Book>> GetAllBooksAsync();
        Task<Guid> UpdateBookAsync(Guid bookId, string ttle, string author, int year, string description);
        Task<Guid?> DeleteBookAsync(Guid bookId);
    }
}
