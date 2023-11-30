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
    public class CompanyRepository: Repository<Company>, ICompanyRepository
    {
        ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company company)
        {
            _db.CompanyTable.Update(company);
        }
    }
}
