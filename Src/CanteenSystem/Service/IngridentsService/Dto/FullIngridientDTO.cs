using Service.AttributeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IngridentsService.Dto
{
    public class FullIngridientDTO
    {
        [ModalHideField]
        public int IngridientId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)]
        public string IngridientName { get; set; }
    }
}
