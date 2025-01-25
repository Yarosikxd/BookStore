namespace Core.Models
{
    public class Book
    {
        private Book(Guid id, string title, string author, int year, string description)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Description = description;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }

        public static Book Create(Guid id, string title, string author, int year, string description)
        {
            return new Book(id, title, author, year, description);
        }
    }
}
