using Service.AttributeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DishService.Dto
{
    public class DishDTO
    {
        [ModalHideField]
        public int DishId { get; set; }

        [ModalFieldType(ModalFieldTypes.Text)]
        public string DishName { get; set; }

        [ModalFieldType(ModalFieldTypes.Number)]
        public int DishCo2 { get; set; }

        [ModalHideField, ModalFieldType(ModalFieldTypes.Checkbox)]
        public bool DishOfTheDay { get; set; }
    }
}
