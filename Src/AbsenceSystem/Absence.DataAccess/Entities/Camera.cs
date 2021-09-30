namespace Absence.DataAccess.Entities
{
    public class Camera
    {
        public string IP { get; set; }
        public int FKClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
