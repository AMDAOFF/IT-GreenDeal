using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.MQTTService
{
    public class IOTSettings
    {
            public string Server { get; init; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string PubTopic { get; set; }
            public string SubTopic { get; set; }
    }
}
