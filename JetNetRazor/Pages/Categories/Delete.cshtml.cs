using JetNetRazor.Data;
using JetNetRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;

namespace JetNetRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
		public readonly ApplicationDbContext _db;
		[BindProperty]
		public Category Category { get; set; }
		public DeleteModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
		{
			Category = _db.CategoryTable.Find(id);
		}
		public IActionResult OnPost(int? id)
		{
			Category = _db.CategoryTable.Find(id);
			_db.CategoryTable.Remove(Category);
			_db.SaveChanges();
			return RedirectToPage("Index");
		}
    }
}
