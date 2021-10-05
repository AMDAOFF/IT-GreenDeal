using System;

namespace Canteen.Service.AttributeService
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModalObjectIdentifierAttribute : Attribute
    {
    }
}
