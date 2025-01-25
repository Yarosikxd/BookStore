using Core.Models;

namespace Core.Abstraction.Repository
{
    public interface IBookRepository
    {
        Task<Guid> CreateBookAsync(Book book);
        Task<Book> GetBookBuIdAsync(Guid bookId);
        Task<List<Book>> GetAllBooksAsync();
        Task<Guid> UpdateBookAsync(Guid bookId, string title, string author, int year, string description);
        Task<Guid> DeleteBookAsync(Guid bookId);
    }
}
