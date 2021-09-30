using Absence.Service.ClassroomService.DataTransferObjects;
using Absence.Service.DayScheduleService.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace Absence.Service.WeekScheduleService.DataTransferObjects
{
    public class FullWeekScheduleDTO
    {
        public int WeekScheduleId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FKClassroomId { get; set; }
        public FullClassroomDTO Classroom { get; set; }
        public List<FullDayScheduleDTO> DaySchedules { get; set; }
    }
}
