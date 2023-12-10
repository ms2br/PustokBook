using System.ComponentModel.DataAnnotations;

namespace PustokBook.ViewModels.SliderVM
{
    public record SliderListItemVm
    {
        [MinLength(5), MaxLength(60)]
        public string AuthorName { get; init; }

        [MinLength(5), MaxLength(40)]
        public string BookName { get; init; }

        public byte Position { get; init; }

        public string ImageUrl { get; init; }
    }
}
