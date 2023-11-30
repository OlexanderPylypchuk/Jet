using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JetFilm.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {
            Company company=new Company();
            if (id == null || id == 0)
            {
                return View(company);
            }
            company=_unitOfWork.Company.GetFirstOrDefault(u=>u.Id==id);
            return View(company);
        }
        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            if(ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                }
                _unitOfWork.Save();
            }
			TempData["Success"] = (obj.Id == null || obj.Id == 0) ? "Successfully added company" : "Successfully updated company";
            return RedirectToAction("Index");
		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Company CompanyFromDb = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
			if (CompanyFromDb is null)
			{
				return NotFound();
			}
			return View(CompanyFromDb);
		}
		[HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
			Company CompanyToBeDeleted = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
			if (CompanyToBeDeleted is null)
			{
				return NotFound();
			}
			_unitOfWork.Company.Delete(CompanyToBeDeleted);
			_unitOfWork.Save();
			TempData["Success"] = "Successfully deleted company";
			return RedirectToAction("Index");
		}
	}
}
