using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class EmployeeForUpdateDto: EmployeeForManipulationDto
    {
        //[Required(ErrorMessage = "Employee Name is a required field.")]
        //[MaxLength(30,ErrorMessage = "Maximum length for name is 30 chars")]
        //public string Name { get; set; }

        //[Range(18,int.MaxValue,ErrorMessage ="Age is required and it can't be lower than 18")]
        //public int Age { get; set; }

        //[Required(ErrorMessage = "Position is a required field.")]
        //[MaxLength(20,ErrorMessage ="Maximum length for the position is 20 chars.")]
        //public string Position { get; set; }
    }
}
