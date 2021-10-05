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
        public DbSet<IngredientAllergy> IngredientAllergies { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserAllergy>()
                .HasKey(ua => new { ua.AllergyId, ua.UserId });

            builder.Entity<UserAllergy>()
                .HasOne(a => a.User)
                .WithMany(a => a.UserAllergies)
                .HasForeignKey(a => a.UserId);


            #region Dish -> DishIngredients
            builder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(di => di.DishIngredients);

            builder.Entity<Ingredient>()
                .HasMany(i => i.DishIngredients)
                .WithOne(i => i.Ingredient);

            builder.Entity<Dish>()
                .HasMany(d => d.DishIngredients)
                .WithOne(d => d.Dish);
            #endregion


            #region Allergies -> IngredientAllergies 
            builder.Entity<IngredientAllergy>()
                .HasKey(ia => new { ia.IngredientId, ia.AllergyId });

            builder.Entity<IngredientAllergy>()
                .HasOne(ia => ia.Allergy)
                .WithMany(ia => ia.IngredientAllergies);

            builder.Entity<Ingredient>()
                .HasMany(i => i.IngredientAllergies)
                .WithOne(i => i.Ingredient);

            builder.Entity<Allergy>()
                .HasMany(a => a.IngredientAllergies)
                .WithOne(a => a.Allergy);
            #endregion



        }

    }
}
