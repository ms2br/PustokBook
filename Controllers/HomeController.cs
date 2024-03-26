using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokBook.Contexts;
using PustokBook.Services.Interfaces;
using PustokBook.ViewModels.BasketVM;
using PustokBook.ViewModels.HomeVM;
using PustokBook.ViewModels.ProductVM;
using PustokBook.ViewModels.SliderVM;

namespace PustokBook.Controllers
{
	public class HomeController : Controller
	{
		PustokDbContexts _db { get; }
		IEmailService _email { get; }
		public HomeController(PustokDbContexts db, IEmailService email)
		{
			_email = email;
			_db = db;
		}

		public async Task<IActionResult> Index()
		{
			List<SliderListItemVm> sliders = await _db.Sliders.Where(a => a.IsDeleted != true).Select(x => new SliderListItemVm
			{
				AuthorName = x.AuthorName,
				BookName = x.BookName,
				ImageUrl = x.ImageUrl,
				Position = Convert.ToByte(x.Position)
			}).ToListAsync();
			List<ProductListItemVM> products = await _db.Products.Where(a => a.IsDeleted != true).Select(x => new ProductListItemVM
			{
				Id = x.Id,
				ActiveImage = x.ActiveImage,
				Authors = x.AuthorBooks.Select(m => m.Author),
				Category = x.Category,
				CategoryId = x.CategoryId,
				Description = x.Description,
				Discount = x.Discount,
				ProductCode = x.ProductCode,
				Quantity = x.Quantity,
				SellPrice = x.SellPrice,
				Tags = x.ProductTags.Select(m => m.Tag),
				Title = x.Title
			}).ToListAsync();
			CollectorVM items = new CollectorVM
			{
				Products = products,
				Sliders = sliders
			};
			return View(items);
		}

		public async Task<IActionResult> BasketAdd(int? id)
		{
			if (id < 1 || id == null) return BadRequest();
			var product = await _db.Products.FindAsync(id);
			if (product == null) return NotFound();

			var baskets = JsonConvert.DeserializeObject<List<BasketProductVMAndCountVM?>>(HttpContext.Request.Cookies["basket"] ?? "[]");

			var existProduct = baskets.Find(x => x.Id == id);

			if (existProduct == null)
			{
				baskets.Add(new BasketProductVMAndCountVM
				{
					Id = product.Id,
					Count = 1
				});
			}
			else
			{
				existProduct.Count++;
			}

			HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(baskets), new CookieOptions
			{
				MaxAge = TimeSpan.MaxValue
			});
			return Ok();
		}

		public IActionResult GetBasket()
		{
			return ViewComponent("Basket");
		}

		public IActionResult BasketRemove(int? id)
		{
			if (id == null || id < 1) return BadRequest();
			var basketProducts = JsonConvert.DeserializeObject<List<BasketProductVMAndCountVM>>(HttpContext.Request.Cookies["basket"] ?? "[]");
			var product = basketProducts.SingleOrDefault(x => x.Id == id);
			if (product == null) return NotFound();

			basketProducts.Remove(product);

			HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketProducts), new CookieOptions
			{
				MaxAge = TimeSpan.MaxValue
			});
			return Ok();
		}

		//public string GetCookies(string key)
		//{
		//    return HttpContext.Request.Cookies[key];
		//}
	}
}
