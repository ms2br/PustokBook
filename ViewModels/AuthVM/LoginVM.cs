using System.ComponentModel.DataAnnotations;

namespace PustokBook.ViewModels.AuthVM
{
    public class LoginVM
    {
        public string UserNameOrUserEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Remember { get; set; } = false;
    }
}
