namespace PustokBook.Models
{
    public class ProducTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public Product? Product { get; set; }
        public Tag? Tag { get; set; }
    }
}
