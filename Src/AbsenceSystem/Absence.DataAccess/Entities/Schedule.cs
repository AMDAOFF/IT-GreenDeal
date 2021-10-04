using System;
using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int FKSubjectId { get; set; }
        public int FKStudentClassId { get; set; }
        public int FKClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public Subject Subject { get; set; }
        public List<AbsenceReport> AbsenceReports { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
