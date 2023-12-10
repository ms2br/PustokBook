using System.ComponentModel.DataAnnotations;

namespace PustokBook.Models
{
    public class Category
    {
        public int Id { get; init; }

        [MinLength(4), MaxLength(16)]
        public string Name { get; set; }

        public int? CategoryId { get; set; }
        public Category? CategoryData { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product>? ProductCategories { get; set; }
    }
}
