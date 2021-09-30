using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Entities
{
    public class WeekSchedule
    {
        public int WeekScheduleId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FKClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public List<DaySchedule> DaySchedules { get; set; }
    }
}
