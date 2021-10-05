namespace Absence.DataAccess.Entities
{
    public class AbsenceReport
    {
        public string FKStudentId { get; set; }
        public int FKScheduleId { get; set; }
        public bool Attended { get; set; }
        public Student Student { get; set; }
        public Schedule Schedule { get; set; }
    }
}
