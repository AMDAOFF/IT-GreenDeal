using Canteen.Service.AttributeService;

namespace Canteen.Service.AllergyService.Dto
{
    public class FullAllergyDTO
    {
        [ModalHideField]
        public int AllergyId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)]
        public string AllergyName { get; set; }
    }
}
