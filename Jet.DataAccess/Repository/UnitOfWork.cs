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
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
            Category = new CategoryRepository(_db);
			Film = new FilmRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
