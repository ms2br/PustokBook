using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.SliderVM
{
    public record CreateAdminSliderVM
    {
        public int Id { get; init; }

        [MinLength(5), MaxLength(60)]
        public string AuthorName { get; init; }

        [MinLength(5), MaxLength(40)]
        public string BookName { get; init; }

        public byte Position { get; init; }

        public bool IsDeleted { get; init; } = false;

        public IFormFile ImageFile { get; set; }
    }
}
