using PustokBook.Models;

namespace PustokBook.ViewModels.ProductVM
{
    public class ProductListItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string ActiveImage { get; set; }
        public decimal SellPrice { get; set; }
        public float Discount { get; set; }
        public ushort Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<Author>? Authors { get; set; }
        public IEnumerable<Tag>? Tags { get; set; }

    }
}
