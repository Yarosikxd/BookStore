using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Books
{
    public record UpdateBookRequest
    (
       [Required] string Title,
       [Required] string Author,
       [Required] int Year,
       [Required] string Description
    );
    
}
