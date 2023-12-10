using System.ComponentModel.DataAnnotations;

namespace PustokBook.Models
{
    public class Slider
    {
        public int Id { get; init; }

        public string ImageUrl { get; set; }

        [MinLength(5), MaxLength(60)]
        public string AuthorName { get; set; }

        [MinLength(5), MaxLength(40)]
        public string BookName { get; set; }

        public bool Position { get; set; }

        public bool IsDeleted { get; set; }
    }
}
