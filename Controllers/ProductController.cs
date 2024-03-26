using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Contexts;

namespace PustokBook.Controllers
{
	public class ProductController : Controller
	{
		PustokDbContexts _db { get; }

		public ProductController(PustokDbContexts db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			ViewData["Categorys"] = _db.Categorys.Include(x => x.ProductCategories);
			ViewData["Tags"] = _db.Tags.Include(x => x.ProducTags);
			return View();
		}

		#region C#
		[HttpPost]
		public IActionResult Index(string? q, List<int>? categoryId, List<int>? tagIds, int? sorted)
		{
			var query = _db.Products.AsQueryable();
			ViewData["Categorys"] = _db.Categorys.Include(x => x.ProductCategories);
			ViewData["Tags"] = _db.Tags.Include(x => x.ProducTags);

			if (!string.IsNullOrWhiteSpace(q))
			{
				query = _db.Products.Where(p => p.Title.Contains(q));
			}

			if (categoryId.Count > 0)
			{
				query = _db.Products.Where(x => categoryId.Contains(x.CategoryId));
			}

			if (tagIds.Count > 0)
			{
				query = _db.Products.Where(x => x.ProductTags.Any(c => tagIds.Contains(c.TagId)));
				//var prodId = _db.ProducTags.Where(x => tagIds.Contains(x.TagId)).Select(x => x.ProductId).AsQueryable();
				//query = query.Where(p => prodId.Contains(p.Id));
			}
			return View(query);
		}
		#endregion

		#region JavaScripts 
		//public IActionResult Index(string? q, List<int>? categoryId, List<int>? tagIds, int? sorted)
		//{
		//	ViewData["Categorys"] = _db.Categorys.Include(x => x.ProductCategories);
		//	ViewData["Tags"] = _db.Tags.Include(x => x.ProducTags);
		//	var query = _db.Products.AsQueryable();

		//	if (string.IsNullOrWhiteSpace(q))
		//	{

		//	}
		//	return View();
		//}
		#endregion
	}
}