using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Books
{
    public record DeleteBookRequest
    (
      [Required] Guid BookId 
    );
    
}
