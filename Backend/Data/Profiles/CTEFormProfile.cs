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
    public class CTEFormProfile : Profile
    {
        //Constructor
        public CTEFormProfile()
        {
            CreateMap<CTEForm, CTEFormDto>()
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentStringify(o.Department)));

            CreateMap<CTEFormDto, CTEForm>()
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentEnumarator(o.Department)));

            CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();

            CreateMap<ExemptedCourse, ExemptedCourseDto>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.CourseType)));

            CreateMap<ExemptedCourseDto, ExemptedCourse>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.CourseType)));

            CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();

            CreateMap<ExemptedCourse, ExemptedCourseDto>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.CourseType)));

            CreateMap<ExemptedCourseDto, ExemptedCourse>()
                .ForMember(d => d.CourseType,
                op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.CourseType)));

            CreateMap<Approval, ApprovalDto>().ReverseMap();
        }
    }
}
