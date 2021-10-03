using Absence.DataAccess.Entities;
using Absence.Service.AbsenceReportService;
using Absence.Service.CameraService;
using Absence.Service.ClassroomService;
using Absence.Service.ScheduleService;
using Absence.Service.SchoolService;
using Absence.Service.StudentClassService;
using Absence.Service.StudentService;
using Absence.Service.SubjectService;
using Absence.Service.TeacherService;
using System;

namespace Absence.Service.AutoMappingService
{

    /// <summary>
    /// The MappingService. Used for Automapper so only 1 mapper is needed.
    /// </summary>
    public class MappingService : LoggingService.LoggingService
    {
        public readonly AutoMapper.IMapper _mapper;
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingService"/> class.
        /// </summary>
        public MappingService()
        {
            AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {

                //AbsenceReport
                cfg.CreateMap<AbsenceReport, FullAbsenceReportDTO>();
                cfg.CreateMap<FullAbsenceReportDTO, AbsenceReport>();

                //Camera
                cfg.CreateMap<Camera, FullCameraDTO>();
                cfg.CreateMap<FullCameraDTO, Camera>();

                //Classroom
                cfg.CreateMap<Classroom, FullClassroomDTO>();
                cfg.CreateMap<FullClassroomDTO, Classroom>();


                //Schedule
                cfg.CreateMap<Schedule, FullScheduleDTO>();
                cfg.CreateMap<FullScheduleDTO, Schedule>();

                //School
                cfg.CreateMap<School, FullSchoolDTO>();
                cfg.CreateMap<FullSchoolDTO, School>();

                //Student
                cfg.CreateMap<Student, FullStudentDTO>();
                cfg.CreateMap<FullStudentDTO, Student>();

                //Subject
                cfg.CreateMap<Subject, FullSubjectDTO>();
                cfg.CreateMap<FullSubjectDTO, Subject>();


                //Teacher
                cfg.CreateMap<Teacher, FullTeacherDTO>();
                cfg.CreateMap<FullTeacherDTO, Teacher>();

                //StudentClass
                cfg.CreateMap<StudentClass, FullStudentClassDTO>();
                cfg.CreateMap<FullStudentClassDTO, StudentClass>();

            });

            try
            {
                _mapper = mapperConfig.CreateMapper();
            }
            catch (Exception ex)
            {
                LogError("Failed to create mappings", ex);
            }

        }
    }
}
