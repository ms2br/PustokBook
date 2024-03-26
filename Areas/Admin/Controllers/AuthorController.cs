using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.ViewModels.AuthorVM;
using PustokBook.Contexts;
using PustokBook.Models;

namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
    public class AuthorController : Controller
    {
        PustokDbContexts _db { get; }

        public AuthorController(PustokDbContexts db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            List<AdminAuthorVM> products = await _db.Authors.Select(x => new AdminAuthorVM
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsDeleted = x.IsDeleted
            }).ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(AdminCreateAndUpdateAuthorVM createAuthor)
        {
            if (!ModelState.IsValid)
                return View(createAuthor);

            await _db.Authors.AddAsync(new Author
            {
                FirstName = createAuthor.FirstName,
                LastName = createAuthor.LastName
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();

            Author author = await _db.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            author.IsDeleted = true;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();

            Author author = await _db.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            AdminCreateAndUpdateAuthorVM data = new AdminCreateAndUpdateAuthorVM
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, AdminCreateAndUpdateAuthorVM updateData)
        {
            if (id < 1 || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(updateData);

            Author author = await _db.Authors.FindAsync(id);

            if (author == null)
                return NotFound();

            author.FirstName = updateData.FirstName;
            author.LastName = updateData.LastName;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
