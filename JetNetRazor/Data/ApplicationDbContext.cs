using JetNetRazor.Models;
using Microsoft.EntityFrameworkCore;

namespace JetNetRazor.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> CategoryTable { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
				new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
				);
		}
	}
}
