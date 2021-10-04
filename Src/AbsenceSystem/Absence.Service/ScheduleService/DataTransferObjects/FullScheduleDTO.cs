using Absence.Service.AbsenceReportService;
using Absence.Service.ClassroomService;
using Absence.Service.StudentClassService;
using Absence.Service.SubjectService;
using System;
using System.Collections.Generic;

namespace Absence.Service.ScheduleService
{
    public class FullScheduleDTO
    {
        public int ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FKSubjectId { get; set; }
        public int FKStudentClassId { get; set; }
        public int FKClassroomId { get; set; }
        public FullClassroomDTO Classroom { get; set; }
        public FullSubjectDTO Subject { get; set; }
        public List<FullAbsenceReportDTO> AbsenceReports { get; set; }
        public FullStudentClassDTO StudentClass { get; set; }
    }
}
