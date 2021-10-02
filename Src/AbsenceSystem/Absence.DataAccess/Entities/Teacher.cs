using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public int FKSubjectId { get; set; }
        public School School { get; set; }
        public Subject Subject { get; set; }
        public List<StudentClass> StudentClasses { get; set; }
    }
}
