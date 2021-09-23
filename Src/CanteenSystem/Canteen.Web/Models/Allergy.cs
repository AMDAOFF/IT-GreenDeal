using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.AttributeService;

namespace Canteen.Web.Models
{
    public class Allergy
    {
        #region Set modal attributes for the property
        [ModalFieldType(ModalFieldTypes.Text)]
        [ModalDisableField]
        [ModalHideField]
        #endregion
        public int Id { get; set; }

        #region Set modal attributes for the property
        [ModalFieldType(ModalFieldTypes.Text)]
        [ModalDisableField]
        #endregion
        public string Name { get; set; }
    }
}
