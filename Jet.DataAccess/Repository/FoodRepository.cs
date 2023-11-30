using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jet.DataAccess.Data;
using Jet.DataAccess.Repository.IRepository;
using Jet.Models;

namespace Jet.DataAccess.Repository
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        ApplicationDbContext _db;
        public FoodRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Update(Food obj)
        {
            _db.FoodTable.Update(obj);
        }
    }
}
