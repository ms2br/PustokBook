using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.SliderVM
{
    public class UpdateAdminSliderVM
    {
        [MinLength(5), MaxLength(60)]
        public string AuthorName { get; init; }

        [MinLength(5), MaxLength(40)]
        public string BookName { get; init; }

        public byte Position { get; init; }

        public IFormFile ImageFile { get; set; }
    }
}
