using Absence.Service.ClassroomService;

namespace Absence.Service.CameraService
{
    public class FullCameraDTO
    {
        public string IP { get; set; }
        public int FKClassroomId { get; set; }
        public FullClassroomDTO Classroom { get; set; }
    }
}
