using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public List<HourSchedule> HourSchedules { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
