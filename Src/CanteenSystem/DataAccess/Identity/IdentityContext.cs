using Canteen.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Canteen.DataAccess.Identity
{
	public class IdentityContext : IdentityDbContext
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Allergy> Allergies { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<Dish> Dishes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<UserAllergy>().HasKey(ua => new {
				ua.UserAllergyId
			});
		}
	}
}
