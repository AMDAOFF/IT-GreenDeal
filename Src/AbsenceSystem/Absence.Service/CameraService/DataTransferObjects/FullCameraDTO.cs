using Absence.Service.ClassroomService.DataTransferObjects;

namespace Absence.Service.CameraService.DataTransferObjects
{
    public class FullCameraDTO
    {
        public string IP { get; set; }
        public int FKClassroomId { get; set; }
        public FullClassroomDTO Classroom { get; set; }
    }
}
