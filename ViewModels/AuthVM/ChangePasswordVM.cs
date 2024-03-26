using System.ComponentModel.DataAnnotations;

namespace PustokBook.ViewModels.AuthVM
{
    public class ChangePasswordVM
    {
        public string OldPassword { get; set; }
        [DataType(DataType.Password), Compare("ConfirmPassword")]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
