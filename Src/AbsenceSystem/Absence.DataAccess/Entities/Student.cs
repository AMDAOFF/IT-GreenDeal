using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<AbsenceReport> AbsenceReports { get; set; }
    }
}
