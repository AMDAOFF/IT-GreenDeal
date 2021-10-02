using Absence.Service.HourScheduleService.DataTransferObjects;
using Absence.Service.StudentService.DataTransferObjects;
using Absence.Service.TeacherService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.StudentClassService.DataTransferObjects
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
        public List<FullHourScheduleDTO> HourSchedules { get; set; }
    }
}
