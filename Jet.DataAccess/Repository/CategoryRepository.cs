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
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(Category obj)
		{
			_db.CategoryTable.Update(obj);
		}
	}
}
