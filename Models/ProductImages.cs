namespace PustokBook.Models
{
    public class ProductImage
    {
        public int Id { get; init; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
