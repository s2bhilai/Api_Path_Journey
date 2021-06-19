using CourseLibrary.API.Helpers;
using CourseLibrary.API.ModelValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [ModelBinder(BinderType = typeof(AuthorEntityBinder))]
    public class AuthorDto_ModelBinding
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [CheckAge(10)]
        public int Age { get; set; }
        public string MainCategory { get; set; }
    }
}
