using Absence.Service.ScheduleService;
using Absence.Service.StudentService;
using Absence.Service.TeacherService;
using System.Collections.Generic;

namespace Absence.Service.StudentClassService
{
    public class FullStudentClassDTO
    {
        public int StudentClassId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The students contact person/teacher
        /// </summary>
        public int FKTeacherId { get; set; }
        public FullTeacherDTO Teacher { get; set; }
        public List<FullStudentDTO> Students { get; set; }
        public List<FullScheduleDTO> Schedules { get; set; }
    }
}
