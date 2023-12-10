using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.ViewModels.Categorys;
using PustokBook.Contexts;
using PustokBook.Models;

namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        PustokDbContexts _db { get; }

        public CategoryController(PustokDbContexts db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var categorys = await _db.Categorys.Select(x => new AdminCategoryListItemVM
            {
                Id = x.Id,
                Name = x.Name,
                CategoryId = x.CategoryId,
                IsDeleted = x.IsDeleted
            }).ToListAsync();
            return View(categorys);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateAdminCategoryVM data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            Category category = new Category
            {
                CategoryId = data.CategoryId,
                Name = data.Name
            };

            await _db.Categorys.AddAsync(category);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();
            Category? category = await _db.Categorys.FindAsync(id);
            if (category == null)
                return NotFound();
            category.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();

            Category category = await _db.Categorys.FindAsync(id);

            if (category == null)
                return NotFound();

            CreateAndUpdateAdminCategoryVM categoryVM = new CreateAndUpdateAdminCategoryVM
            {
                Name = category.Name,
                CategoryId = category.CategoryId
            };

            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, CreateAndUpdateAdminCategoryVM updateData)
        {
            if (id < 1 || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(updateData);

            Category category = await _db.Categorys.FindAsync(id);

            if (category == null)
                return NotFound();

            category.Name = updateData.Name;
            category.CategoryId = updateData.CategoryId;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
