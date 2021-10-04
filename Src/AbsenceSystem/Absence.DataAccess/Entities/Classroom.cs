using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassroomNumber { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public School School { get; set; }
        public List<Schedule> Schedules { get; set; }
        public Camera Camera { get; set; }
    }
}
