using System.ComponentModel.DataAnnotations;

namespace PustokBook.Models
{
    public class Author
    {
        public int Id { get; init; }

        [MinLength(3), MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(25)]
        public string LastName { get; set; }

        public string FullName { get { return this.FirstName + " " + this.LastName; } }

        public List<AuthorProduct>? AuthorBook { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
