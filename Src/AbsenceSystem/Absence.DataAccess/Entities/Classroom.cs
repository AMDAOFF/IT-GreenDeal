using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassroomNumber { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public int FKWeekScheduleId { get; set; }
        //public int FKCameraIP { get; set; }
        public School School { get; set; }
        public List<WeekSchedule> WeekSchedules { get; set; }
        public Camera Camera { get; set; }
    }
}
