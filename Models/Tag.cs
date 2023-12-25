using System.ComponentModel.DataAnnotations;

namespace PustokBook.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [MinLength(3), MaxLength(12)]
        public string Name { get; set; }

        public IEnumerable<ProducTag>? ProducTags { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
