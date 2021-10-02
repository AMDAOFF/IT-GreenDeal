using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Entities
{
    public class StudentClass
    {
        public int StudentClassId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The students contact person/teacher
        /// </summary>
        public int FKTeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }
        public List<HourSchedule> HourSchedules { get; set; }
    }
}
