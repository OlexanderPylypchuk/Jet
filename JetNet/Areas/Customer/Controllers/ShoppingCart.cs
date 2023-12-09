using Jet.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models.ViewModels;

namespace JetFilm.Areas.Customer.Controllers
{
	[Authorize]
	[Area("Customer")]
	public class ShoppingCart : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM ShoppingCartVM { get; set; }
		public ShoppingCart(IUnitOfWork unitOfWork)
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
				ShoppingCartVM.PriceTotal += ticket.Film.Price + ticket.Food.Price;
			}
			ShoppingCartVM.TicketList=ShoppingCartVM.TicketList.OrderBy(u => u.Film.Title);
			return View(ShoppingCartVM);
		}
		public IActionResult Remove(int id)
		{
			var ticketFromDb=_unitOfWork.Ticket.GetFirstOrDefault(u => u.Id == id);
			_unitOfWork.Ticket.Delete(ticketFromDb);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}
	}
}
