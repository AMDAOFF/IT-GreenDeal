using Absence.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Absence.DataAccess.EFCore
{
    public class AbsenceContext : DbContext
    {
        public AbsenceContext(DbContextOptions<AbsenceContext> options) : base(options)
        {

        }

        #region DbSets
        public DbSet<AbsenceReport> AbsenceReports { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Primary Keys
            modelBuilder.Entity<AbsenceReport>().HasKey(o => new { o.FKStudentId, o.FKScheduleId});
            modelBuilder.Entity<Camera>().HasKey(o => o.IP);
            modelBuilder.Entity<Classroom>().HasKey(o => o.ClassroomId);
            modelBuilder.Entity<Schedule>().HasKey(o => o.ScheduleId);
            modelBuilder.Entity<School>().HasKey(o => o.SchoolId);
            modelBuilder.Entity<Student>().HasKey(o => o.StudentId);
            modelBuilder.Entity<Subject>().HasKey(o => o.SubjectId);
            modelBuilder.Entity<Teacher>().HasKey(o => o.TeacherId);
            modelBuilder.Entity<StudentClass>().HasKey(o => o.StudentClassId);
            #endregion

            #region Navigation Properties

            modelBuilder.Entity<AbsenceReport>().HasOne(o => o.Student).WithMany(o => o.AbsenceReports).HasForeignKey(o => o.FKStudentId).OnDelete(DeleteBehavior.Restrict); ;
            modelBuilder.Entity<AbsenceReport>().HasOne(o => o.Schedule).WithMany(o => o.AbsenceReports).HasForeignKey(o => o.FKScheduleId);

            modelBuilder.Entity<Camera>().HasOne(o => o.Classroom).WithOne(o => o.Camera).HasForeignKey<Camera>(o => o.FKClassroomId);

            modelBuilder.Entity<Classroom>().HasOne(o => o.School).WithMany(o => o.Classrooms).HasForeignKey(o => o.FKSchoolId);

            modelBuilder.Entity<Schedule>().HasOne(o => o.Subject).WithMany(o => o.Schedules).HasForeignKey(o => o.FKSubjectId);
            modelBuilder.Entity<Schedule>().HasOne(o => o.StudentClass).WithMany(o => o.Schedules).HasForeignKey(o => o.FKStudentClassId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne(o => o.Classroom).WithMany(o => o.Schedules).HasForeignKey(o => o.FKClassroomId);

            modelBuilder.Entity<Teacher>().HasOne(o => o.School).WithMany(o => o.Teachers).HasForeignKey(o => o.FKSchoolId);

            modelBuilder.Entity<Teacher>().HasOne(o => o.Subject).WithMany(o => o.Teachers).HasForeignKey(o => o.FKSubjectId);

            modelBuilder.Entity<StudentClass>().HasOne(o => o.Teacher).WithMany(o => o.StudentClasses).HasForeignKey(o => o.FKTeacherId);

            modelBuilder.Entity<Student>().HasOne(o => o.StudentClass).WithMany(o => o.Students).HasForeignKey(o => o.FKStudentClassId);
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

            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, Name = "Programmering" },
                new Subject { SubjectId = 2, Name = "Netværk" },
                new Subject { SubjectId = 3, Name = "Engelsk" },
                new Subject { SubjectId = 4, Name = "Dansk" },
                new Subject { SubjectId = 5, Name = "Idræt" },
                new Subject { SubjectId = 6, Name = "Matematik" }
                );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { TeacherId = 1, Name = "Egon Christian Rasmussen", FKSchoolId = 1, FKSubjectId = 1 },
                new Teacher { TeacherId = 2, Name = "Tina Hansen", FKSchoolId = 1, FKSubjectId = 2 },
                new Teacher { TeacherId = 3, Name = "Hans Jørgen Petersen", FKSchoolId = 2, FKSubjectId = 4 },
                new Teacher { TeacherId = 4, Name = "Flemming Nielsen", FKSchoolId = 2, FKSubjectId = 6 }
                );

            modelBuilder.Entity<Student>().HasData(
              new Student { StudentId = "jimm1576", Name = "Jimmy Elkjer", FKStudentClassId = 1 },
              new Student { StudentId = "jona153m", Name = "Jonas Peter Fuhlendorff Jørgensen", FKStudentClassId = 1 },
              new Student { StudentId = "kenn8174", Name = "Kenneth Jessen", FKStudentClassId = 1 },
              new Student { StudentId = "kris593d", Name = "Kristian Biehl Kuhrt", FKStudentClassId = 1 }
              );

            modelBuilder.Entity<StudentClass>().HasData(
                new StudentClass { StudentClassId = 1, Name = "H6 Programmører", FKTeacherId = 1 },
                new StudentClass { StudentClassId = 2, Name = "H2 Infrastruktur", FKTeacherId = 2 },
                new StudentClass { StudentClassId = 3, Name = "8 Klasse", FKTeacherId = 3 }
                );


            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}