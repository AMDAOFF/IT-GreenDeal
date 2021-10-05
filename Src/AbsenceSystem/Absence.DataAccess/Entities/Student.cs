using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Absence.DataAccess.Entities
{
    public class Student
    {
        [MaxLength(8)]
        public string StudentId { get; set; }
        public string Name { get; set; }
        public int FKStudentClassId { get; set; }
        public List<AbsenceReport> AbsenceReports { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
