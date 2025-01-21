namespace Infrastructure.Entities
{
    public class BookEntity
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
    }
}
