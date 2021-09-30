using Absence.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Absence.DataAccess.EFCore
{
    public class AbsenceContext : DbContext
    {
        public AbsenceContext(DbContextOptions<AbsenceContext> options) : base(options)
        {

        }

        public DbSet<AbsenceReport> AbsenceReports { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<DaySchedule> DaySchedules { get; set; }
        public DbSet<HourSchedule> HourSchedules { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<WeekSchedule> WeekSchedules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Keys
            modelBuilder.Entity<AbsenceReport>().HasKey(o => new { o.FKStudentId, o.FKHourScheduleId });
            modelBuilder.Entity<Camera>().HasKey(o => o.IP);
            modelBuilder.Entity<Classroom>().HasKey(o => o.ClassroomId);
            modelBuilder.Entity<DaySchedule>().HasKey(o => o.DayScheduleId);
            modelBuilder.Entity<HourSchedule>().HasKey(o => o.HourScheduleId);
            modelBuilder.Entity<School>().HasKey(o => o.SchoolId);
            modelBuilder.Entity<Student>().HasKey(o => o.StudentId);
            modelBuilder.Entity<Subject>().HasKey(o => o.SubjectId);
            modelBuilder.Entity<Teacher>().HasKey(o => o.TeacherId);
            modelBuilder.Entity<WeekSchedule>().HasKey(o => o.WeekScheduleId);
            #endregion

            #region Navigation Properties
            modelBuilder.Entity<AbsenceReport>().HasOne(o => o.HourSchedule).WithMany(o => o.AbsenceReports).HasForeignKey(o => o.FKHourScheduleId);
            modelBuilder.Entity<AbsenceReport>().HasOne(o => o.Student).WithMany(o => o.AbsenceReports).HasForeignKey(o => o.FKStudentId);

            modelBuilder.Entity<Camera>().HasOne(o => o.Classroom).WithOne(o => o.Camera).HasForeignKey<Camera>(o => o.FKClassroomId);

            //modelBuilder.Entity<Classroom>().HasOne(o => o.Camera).WithOne(o => o.Classroom).HasForeignKey<Classroom>(o => o.FKCameraIP);

            modelBuilder.Entity<Classroom>().HasOne(o => o.School).WithMany(o => o.Classrooms).HasForeignKey(o => o.FKSchoolId);
            
            modelBuilder.Entity<DaySchedule>().HasOne(o => o.WeekSchedule).WithMany(o => o.DaySchedules).HasForeignKey(o => o.FKWeekScheduleId);

            modelBuilder.Entity<HourSchedule>().HasOne(o => o.DaySchedule).WithMany(o => o.HourSchedules).HasForeignKey(o => o.FKDayScheduleId);
            modelBuilder.Entity<HourSchedule>().HasOne(o => o.Subject).WithMany(o => o.HourSchedules).HasForeignKey(o => o.FKSubjectId);

            modelBuilder.Entity<WeekSchedule>().HasOne(o => o.Classroom).WithMany(o => o.WeekSchedules).HasForeignKey(o => o.FKClassroomId);

            modelBuilder.Entity<Teacher>().HasOne(o => o.School).WithMany(o => o.Teachers).HasForeignKey(o => o.FKSchoolId);

            modelBuilder.Entity<Teacher>().HasOne(o => o.Subject).WithMany(o => o.Teachers).HasForeignKey(o => o.FKSubjectId);
            #endregion

            #region Data Seeding
            modelBuilder.Entity<School>().HasData(
                new School { SchoolId = 1, Name = "EUC Syd", Address = "Hilmar Finsens Gade 18, 6400 Sønderborg" },
                new School { SchoolId = 2, Name = "Gråsten Skole", Address = "Degnevænget 2, 6300 Gråsten" }
                );

            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { ClassroomId = 1, ClassroomNumber = "52.211", Name = "Tokyo", FKSchoolId = 1 },
                new Classroom { ClassroomId = 2, ClassroomNumber = "52.212", Name = "Oslo", FKSchoolId = 2 },
                new Classroom { ClassroomId = 3, ClassroomNumber = "51.157", Name = "Hongkong", FKSchoolId = 1 },
                new Classroom { ClassroomId = 4, ClassroomNumber = "51.131", Name = "Paris", FKSchoolId = 2 }
                );

            modelBuilder.Entity<Camera>().HasData(
                new Camera { FKClassroomId = 1, IP = "192.168.0.1" },
                new Camera { FKClassroomId = 3, IP = "192.168.0.2" },
                new Camera { FKClassroomId = 2, IP = "192.168.1.1" },
                new Camera { FKClassroomId = 4, IP = "192.168.1.2" }
                );


            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}