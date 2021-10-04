﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.DeviceService.DTO
{
    public class StatusDeviceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        public bool OnlineStatus { get; set; }             
        public string Classroom { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PeopleCount { get; set; }
        public double Temperature { get; set; }
        public bool RecyclingFan { get; set; }
        public bool VentilationFan { get; set; }
        public bool VentilationValveStatus { get; set; }
        public bool Radiator { get; set; }
        public string DeviceType { get; set; }

        // Device control.
        public bool HeatingStatus { get; set; } = false;
        public bool RecyclingStatus { get; set; } = false;
        public string EnvirementStatus { get; set; }

        // Online ping
        public DateTime OnlinePing { get; set; }
    }
}
