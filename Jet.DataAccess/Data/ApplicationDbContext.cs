using Jet.Models;
using Jet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Jet.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        public DbSet<FilmImage> FilmImageTable { get; set; }
        public DbSet<Category> CategoryTable { get; set; }
		public DbSet<Film> FilmTable { get; set; }
        public DbSet<ApplicationUser> ApplicationUserTable { get; set; }
        public DbSet<Company> CompanyTable { get; set; }
		public DbSet<Ticket> TicketTable { get; set; }
		public DbSet<Food> FoodTable { get; set; }
		public DbSet<Order> OrderTable { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Horror", DisplayOrder = 3 }
                );
			modelBuilder.Entity<Film>().HasData(
				new Film { Id = 1, Title = "Avatar", Description = "Some text about Avatar", Producer = "Some dude", Price = 340, Score = 9.8, CategoryId=1 },
				new Film { Id = 2, Title = "Avatar 2", Description = "Some text about Avatar 2", Producer = "Some dude", Price = 400, Score = 10, CategoryId=2 },
				new Film {
                    Id = 3, Title = "Avatar: The last airbender", Description = "Some text about Avatar: The last airbender", Producer = "Some dude 2", Price = 340, Score = 8.7,
                    CategoryId=3
				},
				new Film { Id = 4, Title = "Drive", Description = "Some text about Drive", Producer = "Some dude", Price = 540, Score = 7 , CategoryId = 1 },
				new Film { Id = 5, Title = "Drive", Description = "Some text about Drive", Producer = "Some dude", Price = 540, Score = 7, CategoryId=2 }
				);
            modelBuilder.Entity<Food>().HasData(
                new Food { Id = -1, Name = "None", Amount = 0, IsFluid = false, Price = 0, ImgUrl = null }
                ); ;
		}
    }
}
