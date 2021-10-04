using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public List<AbsenceReport> AbsenceReports { get; set; }
        public int FKStudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
