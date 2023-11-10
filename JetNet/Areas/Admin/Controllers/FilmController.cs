using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetFilm.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class FilmController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public FilmController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			List<Film> objCategoryList = _unitOfWork.Film.GetAll().ToList();
			return View(objCategoryList);
		}
		public IActionResult Upsert(int? id)
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
			if (id==null || id==0)
			{
				return View(filmVM);
			}
			filmVM.Film=_unitOfWork.Film.GetFirstOrDefault(u => u.Id==id);
			return View(filmVM);
		}
		[HttpPost]
		public IActionResult Upsert(FilmVM obj, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string filmPath = Path.Combine(wwwRootPath, @"images\film");
					if (!string.IsNullOrEmpty(obj.Film.ImgUrl))//видалення старого зображення
					{
						var oldImgUrl = Path.Combine(wwwRootPath, obj.Film.ImgUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImgUrl))
						{
							System.IO.File.Delete(oldImgUrl);
						}
					}
					using (var filestream = new FileStream(Path.Combine(filmPath, filename), FileMode.Create))
					{
						file.CopyTo(filestream);
					}
					obj.Film.ImgUrl = @"\images\Film\" + filename;
				}
				if (obj.Film.Id == 0)
				{
					_unitOfWork.Film.Add(obj.Film);
				}
				else
				{
					_unitOfWork.Film.Update(obj.Film);
				}
				_unitOfWork.Save();
				TempData["Success"] = "Successfully added category";
				return RedirectToAction("Index");
			}
			else
			{
				obj.Category = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(obj);
			}
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
