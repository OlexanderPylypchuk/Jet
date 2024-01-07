using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;

namespace Jet.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _db;
		public ICategoryRepository Category { get; private set; }
		public IFilmRepository Film { get; private set; }
		public IFilmImageRepository FilmImage { get; private set; }
        public ICompanyRepository Company { get; private set; }
		public IFoodRepository Food { get; private set; }
		public ITicketRepository Ticket { get; private set; }
		public IApplicationUserRepository ApplicationUser { get; private set; }
		public IOrderRepository Order { get; private set; }
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
            Category = new CategoryRepository(_db);
			Film = new FilmRepository(_db);
			Company = new CompanyRepository(_db);
			Food = new FoodRepository(_db);
			Ticket = new TicketRepository(_db);
			ApplicationUser = new ApplicationUserRepository(_db);
			Order = new OrderRepository(_db);
			FilmImage = new FilmImageRepository(_db);	
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
