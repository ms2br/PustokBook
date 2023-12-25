using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokBook.Areas.Admin.ViewModels.ProductVM
{
    public class AdminUpdateProductVM
    {
        [MinLength(5), MaxLength(40)]
        public string Title { get; set; }

        [MinLength(5), MaxLength(260)]
        public string Description { get; set; }

        [MinLength(5), MaxLength(20)]
        public string ProductCode { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal ExTax { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal SellPrice { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal CostPrice { get; set; }

        public IFormFile? ActiveImage { get; set; }
        public IEnumerable<IFormFile>? ImagesUrl { get; set; }


        [Range(0, 100)]
        public float Discount { get; set; }
        public ushort Quantity { get; set; }
        public int CategoryId { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public IEnumerable<int>? AuthorIds { get; set; }
        public IEnumerable<int>? TagIds { get; set; }

        public IEnumerable<AdminProductImageVM>? Images { get; set; }
        public string CoverImgUrl { get; set; }
    }
}
