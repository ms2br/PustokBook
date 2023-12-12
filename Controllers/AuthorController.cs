using Microsoft.AspNetCore.Mvc;

namespace PustokBook.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
