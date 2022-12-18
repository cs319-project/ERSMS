using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="RequestedCourseGroup"/> to <see cref="RequestedCourseGroupDto"/> and vice versa.</summary>
    public class RequestedCourseGroupProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="RequestedCourseGroupDto"/> and <see cref="RequestedCourseGroup"/>.</summary>
        public RequestedCourseGroupProfile()
        {
            CreateMap<RequestedCourseGroup, RequestedCourseGroupDto>().ReverseMap();
        }
    }
}
