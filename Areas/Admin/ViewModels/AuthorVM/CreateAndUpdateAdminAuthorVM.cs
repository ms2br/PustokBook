using PustokBook.Models;
using System.ComponentModel.DataAnnotations;

namespace PustokBook.Areas.Admin.ViewModels.AuthorVM
{
    public class CreateAndUpdateAdminAuthorVM
    {
        [MinLength(3), MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3), MaxLength(25)]
        public string LastName { get; set; }

        public List<AuthorProduct>? AuthorBook { get; set; }
        public bool IsDeleted { get; set; }
    }
}
