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
    public class ExemptedCourseProfile : Profile
    {
        // Constructor
        public ExemptedCourseProfile()
        {
            CreateMap<ExemptedCourse, ExemptedCourseDto>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.CourseType)));

            CreateMap<ExemptedCourseDto, ExemptedCourse>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.CourseType)));

        }
    }
}
