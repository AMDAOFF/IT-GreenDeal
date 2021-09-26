using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.MessageService.DTO
{
    public class PublishMessageDTO
    {
        public string RoomNr { get; set; }
        public int PeopleCount { get; set; }
        public DateTime TimeStamp { get; set; }        
    }
}
