using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.Contracts
{
    public class Contracts
    { 
        public record RoomUpdate(string RoomNr, int PeopleCount, DateTime TimeStamp);
    }
}
