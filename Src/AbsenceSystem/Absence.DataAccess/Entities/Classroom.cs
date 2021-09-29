using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Entities
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassroomNumber { get; set; }
        public string Name { get; set; }
        public int FKSchoolId { get; set; }
        public School School { get; set; }
        public Camera Camera { get; set; }
    }
}
