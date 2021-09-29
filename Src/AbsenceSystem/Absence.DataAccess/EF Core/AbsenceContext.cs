using Absence.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Absence.DataAccess.EFCore
{
    public class AbsenceContext : DbContext
    {
        public AbsenceContext(DbContextOptions<AbsenceContext> options) : base(options)
        {

        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Keys
            modelBuilder.Entity<School>().HasKey(o => o.SchoolId);
            modelBuilder.Entity<Classroom>().HasKey(o => o.ClassroomId);
            modelBuilder.Entity<Camera>().HasKey(o => o.IP);
            #endregion

            #region Navigation Properties
            //modelBuilder.Entity<Camera>().HasOne(o => o.School).WithMany(o => o.Cameras).HasForeignKey(o => o.FKSchoolId);
            modelBuilder.Entity<Camera>().HasOne(o => o.Classroom).WithOne(o => o.Camera).HasForeignKey<Camera>(o => o.FKClassroomId);
            modelBuilder.Entity<Classroom>().HasOne(o => o.School).WithMany(o => o.Classrooms).HasForeignKey(o => o.FKSchoolId);
            #endregion

            #region Data Seeding
            modelBuilder.Entity<School>().HasData(
                new School { SchoolId = 1, Name = "EUC Syd", Address = "Hilmar Finsens Gade 18, 6400 Sønderborg" },
                new School { SchoolId = 2, Name = "Gråsten Skole", Address = "Degnevænget 2, 6300 Gråsten" }
                );

            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassroomId = 1, Name = "Tokyo", FKSchoolId = 1 },
                new Classroom { ClassroomId = 2, Name = "Oslo", FKSchoolId = 2 },
                new Classroom { ClassroomId = 3, Name = "Hongkong", FKSchoolId = 1 },
                new Classroom { ClassroomId = 4, Name = "Paris", FKSchoolId = 2 }
                );

            modelBuilder.Entity<Camera>().HasData(
                new Camera { /*FKSchoolId = 1,*/ FKClassroomId = 1, IP = "192.168.0.1" },
                new Camera { /*FKSchoolId = 1,*/ FKClassroomId = 3, IP = "192.168.0.2" },
                new Camera { /*FKSchoolId = 2,*/ FKClassroomId = 2, IP = "192.168.1.1" },
                new Camera { /*FKSchoolId = 2,*/ FKClassroomId = 4, IP = "192.168.1.2" }
                );


            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}