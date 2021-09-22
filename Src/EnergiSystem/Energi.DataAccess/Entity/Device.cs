﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.DataAccess.Entity
{
    public class Device : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool OnlineStatus { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string Classroom { get; set; }
        public double Temperature { get; set; }
        public bool FanOneStatus { get; set; }
        public bool FanTwoStatus { get; set; }
        public bool VentilationValveStatus { get; set; }
    }
}
