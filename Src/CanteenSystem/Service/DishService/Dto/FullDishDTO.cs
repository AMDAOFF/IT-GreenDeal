using Canteen.DataAccess.Models;
using Canteen.Service.AttributeService;
using Canteen.Service.IngridentsService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.DishService.Dto
{
    public class FullDishDTO
    {
        [ModalFieldName("Ret ID"), ModalHideField]
        public int DishId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text), ModalFieldName("Ret Navn")]
        public string DishName { get; set; }

        [ModalFieldType(ModalFieldTypes.Number), ModalFieldName("Rettens CO2 Udskip")]
        public int DishCo2 { get; set; }

        [ModalFieldType(ModalFieldTypes.Checkbox), ModalFieldName("Dagens Menu")]
        public bool DishOfTheDay { get; set; }

        [ModalHideField]
        public List<FullIngridientDTO> Ingredients { get; set; } = new List<FullIngridientDTO>();
    }
}
