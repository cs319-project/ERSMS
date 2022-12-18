using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Entities;
using Backend.DTOs;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Course"/> to <see cref="CourseDto"/> and vice versa.</summary>
    public class CourseProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="CourseDto"/> and <see cref="Course"/>.</summary>
        public CourseProfile()
        {
            CreateMap<CourseDto, Course>().ReverseMap();
        }
    }
}
