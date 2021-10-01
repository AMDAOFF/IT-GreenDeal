﻿// <auto-generated />
using System;
using Absence.DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Absence.DataAccess.Migrations
{
    [DbContext(typeof(AbsenceContext))]
    partial class AbsenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Absence.DataAccess.Entities.AbsenceReport", b =>
                {
                    b.Property<int>("FKStudentId")
                        .HasColumnType("int");

                    b.Property<int>("FKHourScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Attended")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FKStudentId", "FKHourScheduleId");

                    b.HasIndex("FKHourScheduleId");

                    b.ToTable("AbsenceReports");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Camera", b =>
                {
                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FKClassroomId")
                        .HasColumnType("int");

                    b.HasKey("IP");

                    b.HasIndex("FKClassroomId")
                        .IsUnique();

                    b.ToTable("Cameras");

                    b.HasData(
                        new
                        {
                            IP = "192.168.0.1",
                            FKClassroomId = 1
                        },
                        new
                        {
                            IP = "192.168.0.2",
                            FKClassroomId = 3
                        },
                        new
                        {
                            IP = "192.168.1.1",
                            FKClassroomId = 2
                        },
                        new
                        {
                            IP = "192.168.1.2",
                            FKClassroomId = 4
                        });
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Classroom", b =>
                {
                    b.Property<int>("ClassroomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassroomNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FKSchoolId")
                        .HasColumnType("int");

                    b.Property<int>("FKWeekScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClassroomId");

                    b.HasIndex("FKSchoolId");

                    b.ToTable("Classrooms");

                    b.HasData(
                        new
                        {
                            ClassroomId = 1,
                            ClassroomNumber = "52.211",
                            FKSchoolId = 1,
                            FKWeekScheduleId = 0,
                            Name = "Tokyo"
                        },
                        new
                        {
                            ClassroomId = 2,
                            ClassroomNumber = "52.212",
                            FKSchoolId = 2,
                            FKWeekScheduleId = 0,
                            Name = "Oslo"
                        },
                        new
                        {
                            ClassroomId = 3,
                            ClassroomNumber = "51.157",
                            FKSchoolId = 1,
                            FKWeekScheduleId = 0,
                            Name = "Hongkong"
                        },
                        new
                        {
                            ClassroomId = 4,
                            ClassroomNumber = "51.131",
                            FKSchoolId = 2,
                            FKWeekScheduleId = 0,
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.DaySchedule", b =>
                {
                    b.Property<int>("DayScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FKWeekScheduleId")
                        .HasColumnType("int");

                    b.HasKey("DayScheduleId");

                    b.HasIndex("FKWeekScheduleId");

                    b.ToTable("DaySchedules");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.HourSchedule", b =>
                {
                    b.Property<int>("HourScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FKDayScheduleId")
                        .HasColumnType("int");

                    b.Property<int>("FKSubjectId")
                        .HasColumnType("int");

                    b.HasKey("HourScheduleId");

                    b.HasIndex("FKDayScheduleId");

                    b.HasIndex("FKSubjectId");

                    b.ToTable("HourSchedules");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolId");

                    b.ToTable("Schools");

                    b.HasData(
                        new
                        {
                            SchoolId = 1,
                            Address = "Hilmar Finsens Gade 18, 6400 Sønderborg",
                            Name = "EUC Syd"
                        },
                        new
                        {
                            SchoolId = 2,
                            Address = "Degnevænget 2, 6300 Gråsten",
                            Name = "Gråsten Skole"
                        });
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FKSchoolId")
                        .HasColumnType("int");

                    b.Property<int>("FKSubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.HasIndex("FKSchoolId");

                    b.HasIndex("FKSubjectId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.WeekSchedule", b =>
                {
                    b.Property<int>("WeekScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FKClassroomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("WeekScheduleId");

                    b.HasIndex("FKClassroomId");

                    b.ToTable("WeekSchedules");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.AbsenceReport", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.HourSchedule", "HourSchedule")
                        .WithMany("AbsenceReports")
                        .HasForeignKey("FKHourScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Absence.DataAccess.Entities.Student", "Student")
                        .WithMany("AbsenceReports")
                        .HasForeignKey("FKStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HourSchedule");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Camera", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.Classroom", "Classroom")
                        .WithOne("Camera")
                        .HasForeignKey("Absence.DataAccess.Entities.Camera", "FKClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Classroom", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.School", "School")
                        .WithMany("Classrooms")
                        .HasForeignKey("FKSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.DaySchedule", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.WeekSchedule", "WeekSchedule")
                        .WithMany("DaySchedules")
                        .HasForeignKey("FKWeekScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeekSchedule");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.HourSchedule", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.DaySchedule", "DaySchedule")
                        .WithMany("HourSchedules")
                        .HasForeignKey("FKDayScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Absence.DataAccess.Entities.Subject", "Subject")
                        .WithMany("HourSchedules")
                        .HasForeignKey("FKSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DaySchedule");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Teacher", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.School", "School")
                        .WithMany("Teachers")
                        .HasForeignKey("FKSchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Absence.DataAccess.Entities.Subject", "Subject")
                        .WithMany("Teachers")
                        .HasForeignKey("FKSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.WeekSchedule", b =>
                {
                    b.HasOne("Absence.DataAccess.Entities.Classroom", "Classroom")
                        .WithMany("WeekSchedules")
                        .HasForeignKey("FKClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Classroom", b =>
                {
                    b.Navigation("Camera");

                    b.Navigation("WeekSchedules");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.DaySchedule", b =>
                {
                    b.Navigation("HourSchedules");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.HourSchedule", b =>
                {
                    b.Navigation("AbsenceReports");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.School", b =>
                {
                    b.Navigation("Classrooms");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Student", b =>
                {
                    b.Navigation("AbsenceReports");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.Subject", b =>
                {
                    b.Navigation("HourSchedules");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Absence.DataAccess.Entities.WeekSchedule", b =>
                {
                    b.Navigation("DaySchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
