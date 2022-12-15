using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Entities;
using Backend.DTOs;

namespace Backend.Data.Profiles
{
    public class CourseCoordinatorInstructorProfile : Profile
    {
        public CourseCoordinatorInstructorProfile()
        {
            CreateMap<CourseCoordinatorInstructorDto, CourseCoordinatorInstructor>().ReverseMap();
        }
    }
}
