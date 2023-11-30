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
	public class TicketRepository : Repository<Ticket>, ITicketRepository
	{
		private readonly ApplicationDbContext _db;
		public TicketRepository(ApplicationDbContext db): base(db)
		{
			_db = db;
		}

		public void Update(Ticket obj)
		{
			_db.TicketTable.Update(obj);
		}
	}
}
