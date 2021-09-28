using System;

namespace Service.AttributeService
{

    public class ModalFieldTypes
    {
        public const string Text = "text";
        public const string Password = "password";
        public const string Number = "number";
        public const string Checkbox = "checkbox";
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModalFieldTypeAttribute: Attribute
    {
        public string FieldType { get; set; }

        public ModalFieldTypeAttribute(string fieldType)
        {
            this.FieldType = fieldType;
        }
    }
}
