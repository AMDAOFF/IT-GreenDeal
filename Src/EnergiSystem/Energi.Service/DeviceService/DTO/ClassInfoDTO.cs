using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.Service.DeviceService.DTO
{
    public class ClassInfoDTO
    {
        public int Id { get; set; }
        public string Classroom { get; set; }
        public int PeopleCount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
