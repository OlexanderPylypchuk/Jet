using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Jet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetFilm.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
			List<Film> objFilmList = _unitOfWork.Film.GetAll(includeProperties:"Category").ToList();
			return View(objFilmList);
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
				TempData["Success"] = (obj.Film.Id==null|| obj.Film.Id == 0)?"Successfully film":"Successfully updated film";
				return RedirectToAction("Index");
			}
			else
			{
				obj.Category = _unitOfWork.Category.GetAll(null).Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(obj);
			}
		}
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Film> objFilmList = _unitOfWork.Film.GetAll(includeProperties: "Category").ToList();
			return Json(new {data = objFilmList });
		}

		public IActionResult Delete(int? id)
		{
			Film FilmToBeDeleted = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == id);
			if (FilmToBeDeleted is null)
			{
				return Json(new { success = false, message="Error while deleting" });
			}
			var oldImgUrl = Path.Combine(_webHostEnvironment.WebRootPath, FilmToBeDeleted.ImgUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImgUrl))
			{
				System.IO.File.Delete(oldImgUrl);
			}
			_unitOfWork.Film.Delete(FilmToBeDeleted);
			_unitOfWork.Save();
			return Json(new { success=true, message="Successfully deleted" });
		}
		#endregion

	}
}
