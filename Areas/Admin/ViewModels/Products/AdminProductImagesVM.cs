using PustokBook.Models;

namespace PustokBook.Areas.Admin.ViewModels.Products
{
    public class AdminProductImagesVM
    {
        public int Id { get; init; }
        public ICollection<string> ImagesUrl { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
