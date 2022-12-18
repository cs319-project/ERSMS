using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="RequestedCourse"/> to <see cref="RequestedCourseDto"/> and vice versa.</summary>
    public class RequestedCourseProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="RequestedCourseDto"/> and <see cref="RequestedCourse"/>.</summary>
        public RequestedCourseProfile()
        {
            CreateMap<RequestedCourse, RequestedCourseDto>().ReverseMap();
        }
    }
}
