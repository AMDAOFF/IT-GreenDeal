using Canteen.Service.AllergyService.Dto;
using Canteen.Service.AttributeService;
using System.Collections.Generic;

namespace Canteen.Service.IngridentsService.Dto
{
    public class FullIngridientDTO
    {
        [ModalHideField]
        public int IngridientId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)]
        public string IngridientName { get; set; }

        [ModalHideField]
        public List<FullAllergyDTO> Allergies { get; set; }
    }
}
