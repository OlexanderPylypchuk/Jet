using JetNetRazor.Data;
using JetNetRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JetNetRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category{ get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            _db.CategoryTable.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
