using Canteen.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
		public DbSet<UserAllergy> UserAllergies { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<UserAllergy>()
				.HasKey(ua => new { ua.AllergyId, ua.UserId });

			builder.Entity<UserAllergy>()
				.HasOne(a => a.User)
				.WithMany(a => a.UserAllergies)
				.HasForeignKey(a => a.UserId);

			builder.Entity<UserAllergy>()
				.HasOne(a => a.Allergy)
				.WithMany(a => a.UserAllergies)
				.HasForeignKey(a => a.AllergyId);

			//builder.Entity<UserAllergy>().HasKey(ua => new {
			//	ua.UserAllergyId
			//});
		}

	}
}
