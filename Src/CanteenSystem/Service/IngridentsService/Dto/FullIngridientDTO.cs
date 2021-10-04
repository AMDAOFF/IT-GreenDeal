using Canteen.Service.AttributeService;

namespace Canteen.Service.IngridentsService.Dto
{
    public class FullIngridientDTO
    {
        [ModalHideField]
        public int IngridientId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text), ModalFieldName("Ingridiens Navn")]
        public string IngridientName { get; set; }
    }
}
