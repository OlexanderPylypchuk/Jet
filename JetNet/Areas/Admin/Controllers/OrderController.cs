using System.Security.Claims;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Jet.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JetFilm.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class OrderController : Controller
	{
		
		private readonly IUnitOfWork _unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll(string status)
		{
			List<Order> objOrderList = _unitOfWork.Order.GetAll(includeProperties: "User").ToList();

			switch (status)
			{
				case "processing":
					objOrderList = objOrderList.Where(u => u.OrderStatus == SD.StatusProcessing).ToList();
					break;
				case "approved":
					objOrderList = objOrderList.Where(u => u.OrderStatus == SD.StatusApproved).ToList();
					break;
				case "delayed":
					objOrderList = objOrderList.Where(u => u.OrderStatus == SD.StatusDelayed).ToList();
					break;
				default: break;
			}

			return Json(new { data = objOrderList });
		}

		public IActionResult Delete(int? id)
		{
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Order OrderToBeDeleted = _unitOfWork.Order.GetFirstOrDefault(u => u.Id == id);
            if (OrderToBeDeleted is null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var ticketlist = _unitOfWork.Ticket.GetAll().Where(u => u.ApplicationUserId == userId.Value && u.IsBought).ToList();
            foreach (Ticket ticket in ticketlist)
            {
                ticket.IsBought = false;
                _unitOfWork.Ticket.Update(ticket);
            }
			_unitOfWork.Order.Delete(OrderToBeDeleted);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Successfully deleted" });
		}
		#endregion
	}
}
