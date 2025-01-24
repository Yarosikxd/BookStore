namespace Api.Contracts.Books
{
    public record BookResponse
    (
        Guid Id,
        string Title,
        string Author,
        int Year,
        string Description
    );
}
