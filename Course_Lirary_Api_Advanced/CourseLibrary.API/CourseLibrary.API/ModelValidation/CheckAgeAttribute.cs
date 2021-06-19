using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.ModelValidation
{
    public class CheckAgeAttribute: ValidationAttribute
    {
        public CheckAgeAttribute(int age)
        {
            Age = age;
        }

        public int Age { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var author = validationContext.ObjectInstance;

            int age = (int)value;

            if (age < Age)
                return new ValidationResult("Age is Invalid");

            return ValidationResult.Success;
        }
    }
}
