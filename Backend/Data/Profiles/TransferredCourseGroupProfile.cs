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
    /// <summary>A profile for mapping <see cref="TransferredCourseGroup"/> to <see cref="TransferredCourseGroupDto"/> and vice versa.</summary>
    public class TransferredCourseGroupProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="TransferredCourseGroupDto"/> and <see cref="TransferredCourseGroup"/>.</summary>
        public TransferredCourseGroupProfile()
        {
            CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();

            CreateMap<ExemptedCourse, ExemptedCourseDto>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.CourseType)));

            CreateMap<ExemptedCourseDto, ExemptedCourse>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.CourseType)));
        }
    }
}
