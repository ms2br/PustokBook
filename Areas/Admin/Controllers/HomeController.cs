using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Areas.Admin.ViewModels.SliderVM;
using PustokBook.Contexts;
using PustokBook.Models;

namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        PustokDbContexts _dbContexts { get; }
        public HomeController(PustokDbContexts dbContexts)
        {
            _dbContexts = dbContexts;
        }
        public async Task<IActionResult> Index()
        {
            var ProductList = await _dbContexts.Sliders.Select(x => new AdminSliderListItemVM
            {
                Id = x.Id,
                AuthorName = x.AuthorName,
                BookName = x.BookName,
                IsDeleted = x.IsDeleted,
                ImageFile = x.ImageUrl,
                Position = Convert.ToByte(x.Position)
            }).ToListAsync();
            return View(ProductList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminSliderVM itemVM)
        {
            if (itemVM.ImageFile != null)
            {
                if (itemVM.ImageFile.IsValidSize())
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                if (itemVM.ImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }

            }

            if (itemVM.Position > 1 || itemVM.Position < 0)
            {
                ModelState.AddModelError("error", "Wrop Input");
            }


            if (!ModelState.IsValid)
            {

                return View(itemVM);
            }

            await _dbContexts.AddAsync(new Slider
            {
                AuthorName = itemVM.AuthorName,
                BookName = itemVM.BookName,
                ImageUrl = await itemVM.ImageFile.SaveAsync(PathConstants.ProductSlider),
                Position = itemVM.Position switch
                {
                    0 => false,
                    1 => true
                }
            });

            await _dbContexts.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();

            Slider slider = await _dbContexts.Sliders.FindAsync(id);

            if (slider == null)
                return NotFound();

            slider.IsDeleted = true;
            await _dbContexts.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id < 1 || id == null)
                return BadRequest();

            Slider slider = await _dbContexts.Sliders.FindAsync(id);

            if (slider == null)
                return NotFound();

            UpdateAdminSliderVM itemVM = new UpdateAdminSliderVM
            {
                AuthorName = slider.AuthorName,
                BookName = slider.BookName,
                Position = Convert.ToByte(slider.Position)
            };
            return View(itemVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateAdminSliderVM itemData)
        {
            if (id < 1 || id == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(itemData);

            Slider slider = await _dbContexts.Sliders.FindAsync(id);

            if (slider == null)
                return NotFound();

            slider.AuthorName = itemData.AuthorName;
            slider.BookName = itemData.BookName;
            slider.ImageUrl = await itemData.ImageFile.SaveAsync(PathConstants.ProductImage);
            slider.Position = Convert.ToBoolean(itemData.Position);
            await _dbContexts.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
