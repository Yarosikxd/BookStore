using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.Books
{
    public record UpdateBookRequest
    (
       [Required]Guid Id,
       [Required] string Title,
       [Required] string Author,
       [Required] int Year,
       [Required] string Description
    );
    
}
