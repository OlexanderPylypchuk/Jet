using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jet.DataAccess.Data;
using Jet.DataAccess.DBInitializer;
using Jet.Models;
using Jet.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jet.DataAccess.DBInitializer
{
	public class DBInitializer : IDBInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<IdentityUser> _userManager;
		public DBInitializer(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
		{
			_db = db;
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public void Initialize()
		{
			try
			{
				if(_db.Database.GetPendingMigrations().Any())
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex)
			{

			}

			if (!_roleManager.RoleExistsAsync(SD.Role_Cust).GetAwaiter().GetResult())
			{
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Cust)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Comp)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
				_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
				_userManager.CreateAsync(new ApplicationUser
				{
					UserName = "sashylichka2016@gmail.com",
					Email = "sashylichka2016@gmail.com",
					Name = "Pylypchuk Oleksander",
					PhoneNumber = "+380960464868",
					StreetAdress = "Тестова вулиця 1",
					City = "Славута",
					PostalCode = "30000",
				}, "Admin123").GetAwaiter().GetResult();
				ApplicationUser user = _db.ApplicationUserTable.FirstOrDefault(u => u.Email == "sashylichka2016@gmail.com");
				_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
			}
			return;
		}
	}
}
