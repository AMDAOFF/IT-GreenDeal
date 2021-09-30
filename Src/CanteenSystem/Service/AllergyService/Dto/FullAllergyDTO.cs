using Service.AttributeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.AllergyService.Dto
{
    public class FullAllergyDTO
    {
        [ModalHideField]
        public int AllergyId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)]
        public string AllergyName { get; set; }
    }
}
