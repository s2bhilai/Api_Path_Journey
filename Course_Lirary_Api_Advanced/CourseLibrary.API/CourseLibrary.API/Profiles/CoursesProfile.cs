using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace CourseLibrary.API.Profiles
{
    public class CoursesProfile: Profile
    {
        public CoursesProfile()
        {
            CreateMap<Entities.Course,Models.CourseDto>();

            CreateMap<Models.CourseForCreationDto, Entities.Course>();

            CreateMap<Models.CourseForUpdateDto, Entities.Course>().ReverseMap();
        }
    }
}
