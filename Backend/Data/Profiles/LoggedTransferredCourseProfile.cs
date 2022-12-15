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
    public class LoggedTransferredCourseProfile : Profile
    {
        // Constructor
        public LoggedTransferredCourseProfile()
        {
            CreateMap<LoggedTransferredCourse, LoggedTransferredCourseDto>()
                .ForMember(d => d.BilkentCourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.BilkentCourseType)));
            CreateMap<LoggedTransferredCourseDto, LoggedTransferredCourse>()
            .ForMember(d => d.BilkentCourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.BilkentCourseType)));
        }
    }
}
