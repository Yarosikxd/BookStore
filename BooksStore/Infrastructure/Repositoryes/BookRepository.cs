using AutoMapper;
using Core.Abstraction.Repository;
using Core.Models;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositoryes
{
    public class BookRepository : IBookRepository
    {
        public readonly DataBaseDbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(DataBaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> CreateBookAsync(Book book)
        {
           try
            {
                var bookEntity = new BookEntity
                {
                    Id = book.Id,
                    Title = book.Title,
                    Year = book.Year,
                    Author = book.Author,
                    Description = book.Description

                };

                await _context.Books.AddAsync(bookEntity);
                await _context.SaveChangesAsync();

                return bookEntity.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Faiid to create new book", ex);
            }
        }

        public async Task<Guid> DeleteBookAsync(Guid bookId)
        {
            try
            {
                var bookEntity = await _context.Books.FindAsync(bookId);
                if (bookEntity != null) 
                {
                    _context.Books.Remove(bookEntity);
                    await _context.SaveChangesAsync();
                }

                return bookEntity.Id;

            }
            catch (Exception ex) 
            {
                throw new Exception($"Faild to delete bool with id {bookId}", ex);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            try
            {
                var bookEntities = await _context.Books.ToListAsync();
                return bookEntities.Select(b => _mapper.Map<Book>(b)).ToList();
            }
            catch (Exception ex) 
            {
                throw new Exception("Failed to retrieve all books", ex);
            }
        }

        public async Task<Book> GetBookBuIdAsync(Guid bookId)
        {
            try
            {
                BookEntity bookEntity = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.Id == bookId);

                Book book = _mapper.Map<Book>(bookEntity);

                return book;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Failed to retrieve book with id {bookId}", ex);
            }
        }

        public async Task<Guid> UpdateBookAsync(Guid bookId, string title, string author, int year, string description)
        {
            try
            {
                var bookEntity = await _context.Books.FindAsync(bookId);
                if(bookEntity != null)
                {
                    bookEntity.Title = title;
                    bookEntity.Author = author;
                    bookEntity.Description = description;
                    bookEntity.Year = year;
                    bookEntity.Description = description;
                    await _context.SaveChangesAsync();
                }

                return bookId;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Failed to update book with id {bookId}", ex);
            }
        }
    }
}
