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
    /// <summary> A profile for mapping <see cref="LoggedTransferredCourse"/> to <see cref="LoggedTransferredCourseDto"/> and vice versa. </summary>
    public class LoggedTransferredCourseProfile : Profile
    {
        /// <summary> Creates a mapping between the <see cref="LoggedTransferredCourseDto"/> and <see cref="LoggedTransferredCourse"/>. </summary>
        public LoggedTransferredCourseProfile()
        {
            CreateMap<LoggedTransferredCourse, LoggedTransferredCourseDto>();
            // .ForMember(d => d.BilkentCourseType,
            // op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.BilkentCourseType)));
            CreateMap<LoggedTransferredCourseDto, LoggedTransferredCourse>();
            // .ForMember(d => d.BilkentCourseType,
            //     op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.BilkentCourseType)));
        }
    }
}
