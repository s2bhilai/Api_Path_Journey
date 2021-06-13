using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Helpers
{
    public static class DateTimeOffsetExtensions
    {       
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset,
            DateTimeOffset? dateOfD)
        {
            var dateToCalculateTo = DateTime.UtcNow;

            if(dateOfD != null)
            {
                dateToCalculateTo = dateOfD.Value.UtcDateTime;
            }

            int age = dateToCalculateTo.Year - dateTimeOffset.Year;

            if(dateToCalculateTo < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
