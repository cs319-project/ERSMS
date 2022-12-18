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
    /// <summary> A profile for mapping <see cref="LoggedEquivalentCourse"/> to <see cref="LoggedEquivalentCourseDto"/> and vice versa. </summary>
    public class LoggedEquivalentCourseProfile : Profile
    {
        /// <summary> Creates a mapping between the <see cref="LoggedEquivalentCourseDto"/> and <see cref="LoggedEquivalentCourse"/>. </summary>
        public LoggedEquivalentCourseProfile()
        {
            CreateMap<LoggedEquivalentCourse, LoggedEquivalentCourseDto>();
            // .ForMember(d => d.BilkentCourseType,
            // op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.BilkentCourseType)));
            CreateMap<LoggedEquivalentCourseDto, LoggedEquivalentCourse>();
            // .ForMember(d => d.BilkentCourseType,
            //     op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.BilkentCourseType)));
        }
    }
}
