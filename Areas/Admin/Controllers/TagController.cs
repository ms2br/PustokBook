using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.ViewModels.TagVM;
using PustokBook.Contexts;
using PustokBook.Models;

namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        public PustokDbContexts _db { get; set; }

        public TagController(PustokDbContexts db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<AdminTagListItemVM> items = await _db.Tags.Select(x => new AdminTagListItemVM
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted
            }).ToListAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateTagVM data)
        {
            //if (string.IsNullOrWhiteSpace(data.Name)) ModelState.AddModelError("Name", "Name cannot be empty");

            if (!ModelState.IsValid) return View(data);

            await _db.Tags.AddAsync(new Tag
            {
                Name = data.Name
            });

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1 || id == null) return BadRequest();
            Tag? data = await _db.Tags.FindAsync(id);
            if (data == null) return NotFound();
            data.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();
            Tag? item = await _db.Tags.FindAsync(id);
            if (item == null) return NotFound();
            AdminUpdateTagVM data = new AdminUpdateTagVM
            {
                Name = item.Name
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, AdminUpdateTagVM data)
        {
            if (id == null || id < 1) return BadRequest();
            if (!ModelState.IsValid) return View(data);
            Tag? item = await _db.Tags.FindAsync(id);
            if (item == null) return NotFound();
            item.Name = data.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
