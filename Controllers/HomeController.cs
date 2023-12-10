using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Contexts;
using PustokBook.ViewModels.SliderVM;

namespace PustokBook.Controllers
{
    public class HomeController : Controller
    {
        PustokDbContexts _db { get; }
        public HomeController(PustokDbContexts db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            List<SliderListItemVm> listItemVm = await _db.Sliders.Where(a => a.IsDeleted != true).Select(x => new SliderListItemVm
            {
                AuthorName = x.AuthorName,
                BookName = x.BookName,
                ImageUrl = x.ImageUrl,
                Position = Convert.ToByte(x.Position)
            }).ToListAsync();
            return View(listItemVm);
        }
    }
}
