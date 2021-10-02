namespace Absence.DataAccess.Entities
{
    public class AbsenceReport
    {
        public string FKStudentId { get; set; }
        public int FKHourScheduleId { get; set; }
        public string Attended { get; set; }
        public Student Student { get; set; }
        public HourSchedule HourSchedule { get; set; }
    }
}
