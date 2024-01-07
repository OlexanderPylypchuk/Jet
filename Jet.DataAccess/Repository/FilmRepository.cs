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

		public Film GetLast()
		{
			return _db.FilmTable.OrderBy(u=>u.Id).LastOrDefault();
		}

		public void Update(Film obj)
		{
			var objFromDb=_db.FilmTable.FirstOrDefault(u=>u.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.Description = objFromDb.Description;
				objFromDb.Price= objFromDb.Price;
				objFromDb.Producer= objFromDb.Producer;
				objFromDb.CategoryId= objFromDb.CategoryId;
				objFromDb.FilmImages= objFromDb.FilmImages;
				objFromDb.Title= objFromDb.Title;
				objFromDb.Score= objFromDb.Score;
			}
		}
	}
}
