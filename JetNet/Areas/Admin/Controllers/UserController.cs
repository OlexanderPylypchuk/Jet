using System.Security.Claims;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Jet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JetFilm.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class UserController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_roleManager = roleManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult RoleManagement(string id)
		{
			UserVM userVM = new UserVM();
			userVM.User = _unitOfWork.ApplicationUser.GetFirstOrDefault(u=>u.Id==id);
			userVM.UserRoles = _roleManager.Roles.Select(x => new SelectListItem()
			{
				Text=x.Name,
				Value=x.Name
			});
			userVM.Companies = _unitOfWork.Company.GetAll().Select(x => new SelectListItem()
			{
				Text = x.Name,
                Value = x.Id.ToString()
			}) ;
			return View(userVM);
		}
		[HttpPost, ActionName("RoleManagement")]
		public IActionResult RoleAssigment(UserVM userVM)
		{
			string oldRole = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.GetFirstOrDefault((u=>u.Id==userVM.User.Id))).GetAwaiter().GetResult().FirstOrDefault();
			ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => userVM.User.Id == u.Id, includeProperties:"Company");
			if (userVM.User.Role!=oldRole)
			{
				if (userVM.User.Role == SD.Role_Comp)
				{
					user.CompanyId=userVM.User.CompanyId;
				}
				if (oldRole == SD.Role_Comp)
				{
					user.CompanyId = null;
				}
				_unitOfWork.ApplicationUser.Update(user);
				_unitOfWork.Save();
				_userManager.RemoveFromRoleAsync(user, oldRole).GetAwaiter().GetResult();
				_userManager.AddToRoleAsync(user, userVM.User.Role).GetAwaiter().GetResult();
			}
			else if(oldRole == SD.Role_Comp && user.CompanyId != userVM.User.CompanyId)
			{
				user.CompanyId = userVM.User.CompanyId;
				_unitOfWork.ApplicationUser.Update(user);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			List<ApplicationUser> objUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();
			foreach (ApplicationUser user in objUserList)
			{
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
				if (user.Company == null)
				{
					user.Company = new Company() {Name="" };
				}
				if (string.IsNullOrEmpty(user.Name))
				{
					user.Name = "";
				}
				if (user.LockoutEnd == null)
					user.LockoutEnd = DateTime.Now.AddYears(-2000);
			}
			return Json(new { data = objUserList });
		}
		[HttpPost]
		public IActionResult LockUnlock([FromBody]string id)
		{
			var objfromdb= _unitOfWork.ApplicationUser.GetFirstOrDefault(u => id == u.Id);
			if(objfromdb == null)
			{
				return Json(new { success = false, message = "Error while locking a user" });
			}
			string result;
			if (objfromdb.LockoutEnd != null && objfromdb.LockoutEnd > DateTime.Now)
			{
				objfromdb.LockoutEnd= DateTime.Now;
				result = "Successfully unlocked user";
			}
			else
			{
				objfromdb.LockoutEnd= DateTime.Now.AddYears(100);
				result = "Successfully locked user";
			}
			_unitOfWork.ApplicationUser.Update(objfromdb);
			_unitOfWork.Save();
			return Json(new { success = true, message = result });
		}
		public IActionResult Delete(int? id)
		{
			return Json(new { success = true, message = "Successfully deleted" });
		}
		#endregion
	}
}
