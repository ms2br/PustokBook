namespace PustokBook.Areas.Admin.ViewModels.ProductImageVM
{
    public class AdminProductImageListItemVM
    {
        public int Id { get; init; }
        public string ImageUrls { get; set; }
        public string ProductName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
