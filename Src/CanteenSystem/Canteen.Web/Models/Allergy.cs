using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canteen.Web.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Test { get; set; } = 0;
    }
}
