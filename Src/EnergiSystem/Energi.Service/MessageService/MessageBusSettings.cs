using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.MessageService
{
    public class MessageBusSettings
    {
        public string Host { get; init; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Queue { get; set; }
    }
}
