using PustokBook.Models;

namespace PustokBook.Areas.Admin.ViewModels.Products
{
    public class CreateAdminProductImagesVM
    {
        public int Id { get; init; }
        public IEnumerable<IFormFile> ImagesUrl { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
