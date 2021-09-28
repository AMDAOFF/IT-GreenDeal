using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Absence.DataAccess.Entities
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Classroom> Classrooms { get; set; }
        //public List<Camera> Cameras { get; set; }
    }
}
