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
	public class FoodController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FoodController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
            Food food = new Food();
            if (id == null || id == 0)
            {
                return View(food);
            }
            food = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == id);
            return View(food);
        }
        [HttpPost]
        public IActionResult Upsert(Food obj, IFormFile? img)
        {
            if(ModelState.IsValid)
            {
                string wwwrootpath = _webHostEnvironment.WebRootPath;
                if (img != null)
                {
                    string filename=Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    string filepath=Path.Combine(wwwrootpath, @"images\food");
					if (!Directory.Exists(filepath))
					{
						Directory.CreateDirectory(filepath);
					}
					if (!string.IsNullOrEmpty(obj.ImgUrl))//видалення старого зображення
                    {
                        var oldImgUrl = Path.Combine(wwwrootpath, obj.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImgUrl))
                        {
                            System.IO.File.Delete(oldImgUrl);
                        }

                    }
					using (var filestream = new FileStream(Path.Combine(filepath, filename), FileMode.Create))
					{
						img.CopyTo(filestream);
					}
					obj.ImgUrl = @"\images\Food\" + filename;
				}
                if (obj.Id == 0)
                {
                    _unitOfWork.Food.Add(obj);
                }
                else
                {
                    _unitOfWork.Food.Update(obj);
                }
                _unitOfWork.Save();
                TempData["Success"] = (obj.Id == 0) ? "Successfully film" : "Successfully updated film";
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Food> objFoodList = _unitOfWork.Food.GetAll().ToList();
			return Json(new { data = objFoodList });
		}

		public IActionResult Delete(int? id)
		{
			Food FoodToBeDeleted = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == id);
			if (FoodToBeDeleted is null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}
			var oldImgUrl = Path.Combine(_webHostEnvironment.WebRootPath, FoodToBeDeleted.ImgUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImgUrl))
			{
				System.IO.File.Delete(oldImgUrl);
			}
			_unitOfWork.Food.Delete(FoodToBeDeleted);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Successfully deleted" });
		}
		#endregion
	}
}
