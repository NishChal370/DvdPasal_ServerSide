using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DvD_Api.Validators
{
    public class LessThanAttribute: ValidationAttribute
    {
        public string OtherProperty { get; set; }

        public LessThanAttribute(string otherPoperty)
        {
            
            OtherProperty = otherPoperty;
        }

        public override bool RequiresValidationContext
        {
            get
            {
                return true;
            }
        }

        public object DataAnnotationsResources { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

            if (otherPropertyInfo == null) {
                return new ValidationResult("Unknown property");
            }

            if (otherPropertyInfo.PropertyType != typeof(decimal)) {
                // Strictly limit to decimal type only....for now.
                throw new ValidationException($"{OtherProperty} is not a decimal type");
            }

            decimal otherPropertyValue = (decimal)otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (otherPropertyValue > (decimal) value) {
                return null;
            }


            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

        }


    }
}
