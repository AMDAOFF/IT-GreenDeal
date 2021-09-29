using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.MQTTService
{
    public class ConfigMessage
    {
        public int Id { get; set; }
        public bool VentilationFan { get; set; }
        public bool RecyclingFan { get; set; }
        public bool Radiator { get; set; }
        public bool VentilationValveStatus { get; set; }
    }
}
