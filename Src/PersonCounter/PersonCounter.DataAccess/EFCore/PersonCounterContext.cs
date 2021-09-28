using Microsoft.EntityFrameworkCore;

namespace PersonCounter.DataAccess.EFCore
{
    public class PersonCounterContext : DbContext
    {
        public PersonCounterContext(DbContextOptions<PersonCounterContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasKey(o => o.ItemId);
            modelBuilder.Entity<User>().HasKey(o => o.UserId);
            modelBuilder.Entity<Image>().HasKey(o => o.ImageId);
            modelBuilder.Entity<Warehouse>().HasKey(o => o.WarehouseId);
            modelBuilder.Entity<Role>().HasKey(o => o.RoleId);

            modelBuilder.Entity<Item>().HasOne(o => o.Warehouse).WithMany(o => o.Items).HasForeignKey(o => o.FKWarehouseId);
            modelBuilder.Entity<Item>().HasOne(o => o.Image).WithMany(o => o.Items).HasForeignKey(o => o.FKImageId).IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Item>().HasOne(o => o.User).WithMany(o => o.Items).HasForeignKey(o => o.FKUserId);

            modelBuilder.Entity<User>().HasOne(o => o.Role).WithMany(o => o.Users).HasForeignKey(o => o.FKRoleId);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Pallet owner", Description = "A role for pallet owners, does not gain access to anything" },
                new Role { RoleId = 2, Name = "User", Description = "A role for Users, grants access to everything but Warehouses and admin page" },
                new Role { RoleId = 3, Name = "Admin", Description = "A role for Admins, grants access to everything" }
                );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = "F38355", FKRoleId = (int)Repository.Roles.Admin, Email = "lme@danfoss.com", Name = "Laila Mejlvang Ellegaard" },
                new User { UserId = "F12158", FKRoleId = (int)Repository.Roles.User, Email = "kfj@danfoss.com", Name = "Kjeld Frederik Jacobsen" },
                new User { UserId = "F31955", FKRoleId = (int)Repository.Roles.User, Email = "TN@Danfoss.com", Name = "Torben Nielsen" },
                new User { UserId = "U268723", FKRoleId = (int)Repository.Roles.User, Email = "melinda.lonborg@danfoss.com", Name = "Melinda Lønborg" },
                new User { UserId = "F34152", FKRoleId = (int)Repository.Roles.User, Email = "SNS@danfoss.com", Name = "Søren Nykjær Schultz" },
                new User { UserId = "U242141", FKRoleId = (int)Repository.Roles.User, Email = "marold@danfoss.com", Name = "Johnny Marold" },
                new User { UserId = "U338301", FKRoleId = (int)Repository.Roles.Admin, Email = "jimmy@danfoss.com", Name = "Jimmy Elkjer" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
