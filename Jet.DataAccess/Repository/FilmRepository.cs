using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;

namespace Jet.DataAccess.Repository
{
	internal class FilmRepository :Repository<Film>, IFilmRepository
	{
		private readonly ApplicationDbContext _db;
		public FilmRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Film obj)
		{
			_db.FilmTable.Update(obj);
		}
	}
}
