﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class AuthorForCreationWithDateOfDDto:AuthorForCreationDto
    {
        public DateTimeOffset DateOfD { get; set; }
    }
}
