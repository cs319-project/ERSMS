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
    public class LoggedEquivaalanteCourseProfile : Profile
    {
        // Constructor
        public LoggedEquivaalanteCourseProfile()
        {
            CreateMap<LoggedEquivalantCourse, LoggedEquivalantCourseDto>();
            // .ForMember(d => d.BilkentCourseType,
            // op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.BilkentCourseType)));
            CreateMap<LoggedEquivalantCourseDto, LoggedEquivalantCourse>();
            // .ForMember(d => d.BilkentCourseType,
            //     op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.BilkentCourseType)));
        }
    }
}
