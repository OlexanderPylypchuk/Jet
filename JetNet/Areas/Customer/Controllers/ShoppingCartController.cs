using Jet.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models.ViewModels;
using Jet.Utility;
using Microsoft.AspNetCore.Http;

namespace JetFilm.Areas.Customer.Controllers
{
	[Authorize]
	[Area("Customer")]
	public class ShoppingCartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public ShoppingCartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			ShoppingCartVM= new ShoppingCartVM();
		}
		public IActionResult Index()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.TicketList = _unitOfWork.Ticket.GetAll().Where(u => u.ApplicationUserId == userId.Value&&!u.IsBought).ToList();
			foreach (Ticket ticket in ShoppingCartVM.TicketList)
			{
				ticket.Film = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == ticket.FilmId);
				ticket.Food = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == ticket.FoodId);
			}
			ShoppingCartVM.TicketList=ShoppingCartVM.TicketList.OrderBy(u => u.Film.Title);
            return View(ShoppingCartVM);
		}
		[HttpPost, ActionName("Index")]
		public IActionResult IndexPOST()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.TicketList = _unitOfWork.Ticket.GetAll().Where(u => u.ApplicationUserId == userId.Value && !u.IsBought).ToList();
			foreach (Ticket ticket in ShoppingCartVM.TicketList)
			{
				ticket.Film = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == ticket.FilmId);
				ticket.Food = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == ticket.FoodId);
			}
			ShoppingCartVM.Order = _unitOfWork.Order.GetFirstOrDefault(u => u.User.Id == userId.Value, includeProperties:"User");
			if (ShoppingCartVM.Order == null)
			{
				ShoppingCartVM.Order = new Order();
				ShoppingCartVM.Order.User = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId.Value);
				foreach (var ticket in ShoppingCartVM.TicketList)
				{
					ticket.IsBought = true;
					_unitOfWork.Ticket.Update(ticket);
					ShoppingCartVM.Order.PriceTotal += ticket.Film.Price + ticket.Food.Price;
				}
				ShoppingCartVM.Order.OrderingDate = DateTime.Now;
				ShoppingCartVM.Order.PaymentDueDate = DateTime.Now.AddDays(1);
				ShoppingCartVM.Order.ApplicationUserId = userId.Value;
				if(ShoppingCartVM.Order.User.CompanyId==0)
				{
					ShoppingCartVM.Order.OrderStatus = SD.StatusPending;
				}
				else ShoppingCartVM.Order.OrderStatus= SD.StatusDelayed;
				_unitOfWork.Order.Add(ShoppingCartVM.Order);
                HttpContext.Session.SetInt32(SD.SessionCart, ShoppingCartVM.TicketList.Count());
            }
			else
			{
				foreach (var ticket in ShoppingCartVM.TicketList)
				{
					ticket.IsBought = true;
					_unitOfWork.Ticket.Update(ticket);
					ShoppingCartVM.Order.PriceTotal += ticket.Film.Price + ticket.Food.Price;
				}
				_unitOfWork.Order.Update(ShoppingCartVM.Order);
			}
			_unitOfWork.Save();
			return RedirectToAction("Summary");
		}
		public IActionResult Remove(int id)
		{
            var ticketFromDb=_unitOfWork.Ticket.GetFirstOrDefault(u => u.Id == id);
			_unitOfWork.Ticket.Delete(ticketFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
		}
		public IActionResult Summary()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.Order = _unitOfWork.Order.GetFirstOrDefault(u => u.ApplicationUserId == userId.Value);
			ShoppingCartVM.TicketList = _unitOfWork.Ticket.GetAll().Where(u => u.ApplicationUserId == userId.Value && u.IsBought).ToList();
			foreach (Ticket ticket in ShoppingCartVM.TicketList)
			{
				ticket.Film = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == ticket.FilmId);
				ticket.Food = _unitOfWork.Food.GetFirstOrDefault(u => u.Id == ticket.FoodId);
			}
			ShoppingCartVM.TicketList = ShoppingCartVM.TicketList.OrderBy(u => u.Film.Title);
			return View(ShoppingCartVM);
		}
		public IActionResult DeleteOrder()
		{
			var claimIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
			ShoppingCartVM.TicketList= ShoppingCartVM.TicketList = _unitOfWork.Ticket.GetAll().Where(u => u.ApplicationUserId == userId.Value && u.IsBought).ToList();
            Order order = _unitOfWork.Order.GetFirstOrDefault(u => u.ApplicationUserId == userId.Value);
			foreach(Ticket ticket in ShoppingCartVM.TicketList)
			{
				ticket.IsBought=false;
				_unitOfWork.Ticket.Update(ticket);
			}
			_unitOfWork.Order.Delete(order);
			_unitOfWork.Save();
			return Redirect("/Customer/Home/Index");
		}
	}
}
