using JetNetRazor.Data;
using JetNetRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JetNetRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
		public readonly ApplicationDbContext _db;
		[BindProperty]
		public Category Category { get; set; }
		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
		{
			Category = _db.CategoryTable.Find(id);
		}
		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				_db.CategoryTable.Update(Category);
				_db.SaveChanges();
				//TempData["Success"] = "Successfully updated category";
				return RedirectToPage("Index");
			}
			return Page();
		}
	}
}
