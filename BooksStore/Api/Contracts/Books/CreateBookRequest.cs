using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Books
{
    public record CreateBookRequest
    (
       [Required] string Title,
       [Required] string Author,
       [Required] int Year,
       [Required] string Description
    );
    
    
}
