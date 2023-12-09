using System.Diagnostics;
using System.Security.Claims;
using Humanizer;
using Jet.DataAccess.Repository;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;
using Jet.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JetFilm.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Film> filmList = _unitOfWork.Film.GetAll(includeProperties: "Category").ToList();
            return View(filmList);
        }

		public IActionResult Details(int? id)
		{
			Film film = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category");
			return View(film);
		}
        [HttpPost]
        public IActionResult Details()
        {
            return RedirectToAction(nameof(BuyTicket));
        }
        public IActionResult BuyTicket(int? id)
        {
            if(id == null||id==0)
            {
                RedirectToAction(nameof(Index));
            }
            Film Film = _unitOfWork.Film.GetFirstOrDefault(u => u.Id == id);
            IEnumerable<SelectListItem> Food = _unitOfWork.Food.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            TicketVM ticketVM = new TicketVM()
            {
                Film = Film,
                FoodList = Food,
                Ticket = new Ticket()
            };
			return View(ticketVM);

		}
        [HttpPost]
        [Authorize]
		public IActionResult BuyTicket(TicketVM ticketVM)
		{
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId=claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ticketVM.Ticket.FilmId=ticketVM.Film.Id;
            if(ModelState.IsValid)
            {
                List<Ticket> ExistingTickets=_unitOfWork.Ticket.GetAll().ToList();
                if(ExistingTickets.Any(u=>u.FilmId==ticketVM.Film.Id&&
                u.Place == ticketVM.Ticket.Place))
                {
					TempData["Error"] = "This place is not available";
					return RedirectToAction(nameof(Index));
				}
				ticketVM.Ticket.ApplicationUserId = userId.Value;
				ticketVM.Ticket.Code = Guid.NewGuid().ToString();
                _unitOfWork.Ticket.Add(ticketVM.Ticket);
                _unitOfWork.Save();
				TempData["Success"] = "Successfully bougth ticket! Enjoy your movie!";
				return RedirectToAction(nameof(Index));
			}
			return View(ticketVM);

		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}