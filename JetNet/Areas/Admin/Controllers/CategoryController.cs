using System.Collections.Generic;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == "Підарас")
            {
                ModelState.AddModelError("", "That cannot be the name of Category");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Successfully added category";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == "Підарас")
            {
                ModelState.AddModelError("", "That cannot be the name of Category");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
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
            Category categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDb is null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Delete(categoryFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Successfully deleted category";
            return RedirectToAction("Index");
        }
    }
}
