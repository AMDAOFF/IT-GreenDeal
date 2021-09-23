using System;

namespace Service.AttributeService
{

    public enum ModalFieldTypes
    {
        Text,
        Password,
        Number,
        Hidden
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModalFieldTypeAttribute: Attribute
    {
        public ModalFieldTypeAttribute(ModalFieldTypes fieldType)
        {

        }
    }
}
