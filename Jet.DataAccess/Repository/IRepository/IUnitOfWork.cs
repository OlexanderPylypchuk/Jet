using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jet.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
	{
		ICategoryRepository Category { get; }
		IFilmRepository Film { get; }
		IFilmImageRepository FilmImage { get; }
		ICompanyRepository Company { get; }
		IFoodRepository Food { get; }
		ITicketRepository Ticket { get; }
		IApplicationUserRepository ApplicationUser { get; }
		IOrderRepository Order { get; }
		void Save();
	}
}
