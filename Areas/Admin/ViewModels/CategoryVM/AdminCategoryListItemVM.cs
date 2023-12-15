using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.CategoryVM
{
    public class AdminCategoryListItemVM
    {
        public int Id { get; init; }

        [MinLength(4), MaxLength(16)]
        public string Name { get; set; }

        public int? CategoryId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
