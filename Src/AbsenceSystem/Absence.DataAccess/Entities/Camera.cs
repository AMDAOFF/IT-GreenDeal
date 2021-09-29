using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Entities
{
    public class Camera
    {
        public string IP { get; set; }
        //public int FKSchoolId { get; set; }
        //public School School { get; set; }
        public int FKClassroomId { get; set; }
        public Classroom Classroom { get; set; }
    }
}
