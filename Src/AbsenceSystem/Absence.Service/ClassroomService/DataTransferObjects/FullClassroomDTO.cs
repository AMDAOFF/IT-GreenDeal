using Absence.Service.CameraService.DataTransferObjects;
using Absence.Service.SchoolService.DataTransferObjects;
using Absence.Service.WeekScheduleService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.ClassroomService.DataTransferObjects
{
    public class FullClassroomDTO
    {
        public int ClassroomId { get; set; }
        public string ClassroomNumber { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public int FKWeekScheduleId { get; set; }
        public FullSchoolDTO School { get; set; }
        public List<FullWeekScheduleDTO> WeekSchedules { get; set; }
        public FullCameraDTO Camera { get; set; }
    }
}
