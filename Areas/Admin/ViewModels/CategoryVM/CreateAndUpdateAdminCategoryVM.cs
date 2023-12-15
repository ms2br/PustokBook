using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.CategoryVM
{
    public class CreateAndUpdateAdminCategoryVM
    {
        [MinLength(4), MaxLength(16)]
        public string Name { get; set; }

        public int? CategoryId { get; set; }
    }
}
