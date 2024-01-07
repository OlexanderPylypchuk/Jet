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
			return View();
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
			filmVM.Film=_unitOfWork.Film.GetFirstOrDefault(u => u.Id==id, includeProperties:"FilmImages");
			return View(filmVM);
		}
		[HttpPost]
		public IActionResult Upsert(FilmVM obj, List<IFormFile> files)
		{
			if (ModelState.IsValid)
			{
				if (obj.Film.Id == 0)
				{
					_unitOfWork.Film.Add(obj.Film);
					_unitOfWork.Save();
					obj.Film = _unitOfWork.Film.GetLast();

				}
				else
				{
					_unitOfWork.Film.Update(obj.Film);
				}

				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (files != null)
				{
					foreach (IFormFile file in files) {
						string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
						string filmPath=@"images\film\film-"+obj.Film.Id;
						string finalPath = Path.Combine(wwwRootPath, filmPath);

						if(!Directory.Exists(finalPath))
						{
							Directory.CreateDirectory(finalPath);
						}

						using (var filestream = new FileStream(Path.Combine(finalPath, filename), FileMode.Create))
						{
							file.CopyTo(filestream);
						}

						FilmImage filmImage = new FilmImage()
						{
							ImgUrl = @"\" + filmPath + @"\" + filename,
							FilmId = obj.Film.Id
						};

						if (obj.Film.FilmImages == null)
						{
							obj.Film.FilmImages=new List<FilmImage> ();
						}

						obj.Film.FilmImages.Add(filmImage);
						_unitOfWork.FilmImage.Add(filmImage);
					}
				}
				_unitOfWork.Film.Update(obj.Film);
				_unitOfWork.Save();
				TempData["Success"] = "Successfully created/updated film";
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
		public IActionResult DeleteImage(int imageId)
		{
			FilmImage image = _unitOfWork.FilmImage.GetFirstOrDefault(u => u.Id == imageId);
			int? filmid=image.FilmId;
			if (image != null)
			{
				if (!string.IsNullOrEmpty(image.ImgUrl))
				{
					string imageurl = Path.Combine(_webHostEnvironment.WebRootPath, image.ImgUrl.TrimStart('\\'));
					if (System.IO.File.Exists(imageurl))
					{
						System.IO.File.Delete(imageurl);
					}
					
				}
				_unitOfWork.FilmImage.Delete(image);
				_unitOfWork.Save();
				TempData["success"] = "Image deleted successfully";
			}
			return RedirectToAction(nameof(Upsert),new {id= filmid } );
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
			var filmpath = @"images\film\film-"+id;
			string finalpath = Path.Combine(_webHostEnvironment.WebRootPath, filmpath);
			if (Directory.Exists(finalpath))
			{
				string[] filepaths= Directory.GetFiles(finalpath);
				foreach (string filepath in filepaths)
				{
					System.IO.File.Delete(filepath);
				}
				Directory.Delete(finalpath);
			}
			_unitOfWork.Film.Delete(FilmToBeDeleted);
			_unitOfWork.Save();
			return Json(new { success=true, message="Successfully deleted" });
		}
		#endregion

	}
}
