using PustokBook.Models;


namespace PustokBook.Areas.Admin.ViewModels.Products
{
    public class AdminProductListItemVM
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ProductCode { get; set; }

        public decimal ExTax { get; set; }

        public string ActiveImage { get; set; }

        public decimal SellPrice { get; set; }

        public decimal CostPrice { get; set; }

        public float Discount { get; set; }
        public ushort Quantity { get; set; }

        public IEnumerable<Author>? Authors { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}
