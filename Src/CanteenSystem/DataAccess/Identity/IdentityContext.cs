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

			builder.Entity<Dish>()
				.HasData(
					new Dish
					{
						DishId = 1,
						DishName = "Tomatsuppe",
						DishCO2 = 500,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 2,
						DishName = "Frikardeller",
						DishCO2 = 650,
						DishOfTheDay = true
					},
					new Dish
					{
						DishId = 3,
						DishName = "Græsk Farsbrød",
						DishCO2 = 445,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 4,
						DishName = "Hakkebøf Med Løg",
						DishCO2 = 700,
						DishOfTheDay = true
					},
					new Dish
					{
						DishId = 5,
						DishName = "Smørrebrød",
						DishCO2 = 250,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 6,
						DishName = "Pasta Kødsovs",
						DishCO2 = 800,
						DishOfTheDay = true
					},
					new Dish
					{
						DishId = 7,
						DishName = "Boller I Karry",
						DishCO2 = 650,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 8,
						DishName = "Koteletter I Fad",
						DishCO2 = 520,
						DishOfTheDay = true
					},
					new Dish
					{
						DishId = 9,
						DishName = "Gule Ærter",
						DishCO2 = 580,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 10,
						DishName = "Svensk Pølseret",
						DishCO2 = 150,
						DishOfTheDay = true
					},
					new Dish
					{
						DishId = 11,
						DishName = "Æggekage",
						DishCO2 = 100,
						DishOfTheDay = false
					},
					new Dish
					{
						DishId = 12,
						DishName = "Fiskefrikardeller",
						DishCO2 = 403,
						DishOfTheDay = false
					}
				);

			builder.Entity<Ingredient>()
				.HasData(
					new Ingredient
					{
						IngredientId = 1,
						IngredientName = "Fladfisk"
					},
					new Ingredient
					{
						IngredientId = 2,
						IngredientName = "Tomatsovs"
					},
					new Ingredient
					{
						IngredientId = 3,
						IngredientName = "Løg"
					},
					new Ingredient
					{
						IngredientId = 4,
						IngredientName = "Æg"
					},
					new Ingredient
					{
						IngredientId = 5,
						IngredientName = "Oksefars"
					},
					new Ingredient
					{
						IngredientId = 6,
						IngredientName = "Pasta"
					},
					new Ingredient
					{
						IngredientId = 7,
						IngredientName = "Korteletter (svin)"
					},
					new Ingredient
					{
						IngredientId = 8,
						IngredientName = "Fetaost"
					},
					new Ingredient
					{
						IngredientId = 9,
						IngredientName = "Kødboller"
					},
					new Ingredient
					{
						IngredientId = 10,
						IngredientName = "Salt"
					},
					new Ingredient
					{
						IngredientId = 11,
						IngredientName = "Pebber"
					},
					new Ingredient
					{
						IngredientId = 12,
						IngredientName = "Bacon"
					},
					new Ingredient
					{
						IngredientId = 13,
						IngredientName = "Kartofler"
					},
					new Ingredient
					{
						IngredientId = 14,
						IngredientName = "Oksebullion"
					}
				);

			builder.Entity<Allergy>()
				.HasData(
					new Allergy
					{
						AllergyId = 1,
						AllergyName = "Gluten Allergi"
					},
					new Allergy
					{
						AllergyId = 2,
						AllergyName = "Grøntsag"
					},
					new Allergy
					{
						AllergyId = 3,
						AllergyName = "Skaldyr"
					},
					new Allergy
					{
						AllergyId = 4,
						AllergyName = "Laktose"
					},
					new Allergy
					{
						AllergyId = 5,
						AllergyName = "Nødder"
					}
				);
		}

	}
}
