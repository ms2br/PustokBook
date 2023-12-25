using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.TagVM
{
    public class AdminCreateTagVM
    {
        [MinLength(3), MaxLength(12)]
        public string Name { get; set; }
    }
}
