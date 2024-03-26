using Microsoft.AspNetCore.Identity;

namespace PustokBook.Models
{
    public class User : IdentityUser
    {
        public string ProfilActiveImageUrl { get; set; }
    }
}
