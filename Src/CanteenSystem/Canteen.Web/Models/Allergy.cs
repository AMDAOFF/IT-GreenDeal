using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.AttributeService;

namespace Canteen.Web.Models
{
    public class Allergy
    {
        [ModalFieldType(ModalFieldTypes.Hidden)] // Set the field type for the edit modal for this property
        public int Id { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)] // Set the field typåe for the edit modal for this property
        public string Name { get; set; }
    }
}
