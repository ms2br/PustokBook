using System.ComponentModel.DataAnnotations;

namespace PustokBook.ViewModels.AuthVM
{
    public class ResetPasswordVM
    {
        [Required, DataType(DataType.Password), MinLength(4)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
