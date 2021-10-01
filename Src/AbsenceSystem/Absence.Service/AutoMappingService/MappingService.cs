using Absence.DataAccess.Entities;
using Absence.Service.AbsenceReportService.DataTransferObjects;
using Absence.Service.CameraService.DataTransferObjects;
using Absence.Service.ClassroomService.DataTransferObjects;
using Absence.Service.DayScheduleService.DataTransferObjects;
using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.SchoolService.DataTransferObjects;
using Absence.Service.StudentService.DataTransferObjects;
using Absence.Service.SubjectService.DataTransferObjects;
using Absence.Service.TeacherService.DataTransferObjects;
using Absence.Service.WeekScheduleService.DataTransferObjects;
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

                //DaySchedule
                cfg.CreateMap<DaySchedule, FullDayScheduleDTO>();
                cfg.CreateMap<FullDayScheduleDTO, DaySchedule>();

                //HourSchedule
                cfg.CreateMap<HourSchedule, FullHourScheduleDTO>();
                cfg.CreateMap<FullHourScheduleDTO, HourSchedule>();

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

                //WeekSchedule
                cfg.CreateMap<WeekSchedule, FullWeekScheduleDTO>();
                cfg.CreateMap<FullWeekScheduleDTO, WeekSchedule>();


            });

            try
            {
                _mapper = mapperConfig.CreateMapper();
                LogInformation("Successfully created mappings");
            }
            catch (Exception ex)
            {
                LogError("Failed to create mappings", ex);
            }

        }
    }
}
