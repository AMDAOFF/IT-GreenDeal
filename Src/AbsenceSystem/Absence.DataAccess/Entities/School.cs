﻿using System.Collections.Generic;

namespace Absence.DataAccess.Entities
{
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Classroom> Classrooms { get; set; }
        public List<Teacher> Teachers { get; set; }
    }
}
