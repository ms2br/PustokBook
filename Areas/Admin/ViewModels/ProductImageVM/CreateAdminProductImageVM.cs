namespace PustokBook.Areas.Admin.ViewModels.ProductImageVM
{
    public class CreateAdminProductImageVM
    {
        public IFormFile ImageUrl { get; set; }
        public int ProductId { get; set; }
    }
}
