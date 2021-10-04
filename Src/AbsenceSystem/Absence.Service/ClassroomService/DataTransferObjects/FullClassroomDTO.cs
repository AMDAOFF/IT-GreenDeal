using Absence.Service.CameraService;
using Absence.Service.SchoolService;
using System.Collections.Generic;

namespace Absence.Service.ClassroomService
{
    public class FullClassroomDTO
    {
        public int ClassroomId { get; set; }
        public string ClassroomNumber { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public int FKWeekScheduleId { get; set; }
        public FullSchoolDTO School { get; set; }
        public FullCameraDTO Camera { get; set; }
    }
}
