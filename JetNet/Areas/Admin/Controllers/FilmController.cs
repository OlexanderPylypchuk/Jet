using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetBook.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class FilmController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public FilmController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<Film> objCategoryList = _unitOfWork.Film.GetAll().ToList();
			return View(objCategoryList);
		}
		public IActionResult Create()
		{
			IEnumerable<SelectListItem> Category = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			FilmVM filmVM = new()
			{
				Category = Category,
				Film = new Film()
			};
			return View(filmVM);
		}
		[HttpPost]
		public IActionResult Create(FilmVM obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Film.Add(obj.Film);
				_unitOfWork.Save();
				TempData["Success"] = "Successfully added category";
				return RedirectToAction("Index");
			}
			else
			{
				obj.Category= _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View();
			}
		}
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Film? filmfromdb = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == id);
			if (filmfromdb is null)
			{
				return NotFound();
			}
			return View(filmfromdb);
		}
		[HttpPost]
		public IActionResult Edit(Film obj)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Film.Update(obj);
				_unitOfWork.Save();
				TempData["Success"] = "Successfully updated category";
				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Film? filmfromdb = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == id);
			if (filmfromdb is null)
			{
				return NotFound();
			}
			return View(filmfromdb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Film? filmfromdb=_unitOfWork.Film.GetFirstOrDefault(u=>u.Id == id);
			if (filmfromdb is null)
			{
				return NotFound();
			}
			_unitOfWork.Film.Delete(filmfromdb);
			_unitOfWork.Save();
			TempData["Success"] = "Successfully deleted category";
			return RedirectToAction("Index");
		}
	}
}
