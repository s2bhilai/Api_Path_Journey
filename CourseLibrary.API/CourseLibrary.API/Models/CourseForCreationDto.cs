using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{

    public class CourseForCreationDto : CourseForManipulationDto //: IValidatableObject
    {


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if(Title == Description)
        //    {
        //        yield return new ValidationResult(
        //            "The provided description should be different from title",
        //            new[] { "CourseForCreationDto" });
        //    }
        //}
        
    }
}
