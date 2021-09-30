using Absence.Service.ClassroomService.DataTransferObjects;
using Absence.Service.TeacherService.DataTransferObjects;
using System.Collections.Generic;

namespace Absence.Service.SchoolService.DataTransferObjects
{
    public class FullSchoolDTO
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<FullClassroomDTO> Classrooms { get; set; }
        public List<FullTeacherDTO> Teachers { get; set; }
    }
}
