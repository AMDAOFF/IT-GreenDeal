using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.AttributeService
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModalFieldNameAttribute : Attribute
    {
        public string FieldName { get; set; }
        public ModalFieldNameAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
