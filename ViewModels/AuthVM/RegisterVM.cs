using System.ComponentModel.DataAnnotations;

namespace PustokBook.ViewModels.AuthVM
{
    public class RegisterVM
    {
        [MaxLength(15), MinLength(3)]
        public string UserName { get; set; }
        [MaxLength(254), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MinLength(4), MaxLength(254), DataType(DataType.Password), Compare(nameof(ConfirmedPassword))]
        public string Password { get; set; }

        [MinLength(4), MaxLength(254), DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }

        public IFormFile ProfilActiveImageUrl { get; set; }
    }
}
