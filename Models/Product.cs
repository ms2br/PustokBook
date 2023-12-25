using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBook.Models
{
    public class Product
    {
        public int Id { get; init; }

        [MinLength(5), MaxLength(40)]
        public string Title { get; set; }

        [MinLength(5), MaxLength(260)]
        public string Description { get; set; }

        [MinLength(5), MaxLength(20)]
        public string ProductCode { get; set; }

        public string ActiveImage { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal ExTax { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }

        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<AuthorProduct>? AuthorBooks { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProducTag>? ProductTags { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
