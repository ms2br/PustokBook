namespace PustokBook.Models
{
    public class AuthorBook
    {
        public int Id { get; init; }
        public int AuthorId { get; set; }
        public int ProductId { get; set; }
        public Author? Author { get; set; }
        public Product? Product { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
