using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="CTEForm"/> to <see cref="CTEFormDto"/> and vice versa.</summary>
    public class CTEFormProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="CTEFormDto"/> and <see cref="CTEForm"/>.</summary>
        public CTEFormProfile()
        {
            CreateMap<CTEForm, CTEFormDto>()
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentStringify(o.Department)));

            CreateMap<CTEFormDto, CTEForm>()
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentEnumarator(o.Department)));

            // CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();

            // CreateMap<ExemptedCourse, ExemptedCourseDto>()
            //     .ForMember(d => d.CourseType,
            //     op => op.MapFrom(o => EnumStringify.CourseTypeStringify(o.CourseType)));

            // CreateMap<ExemptedCourseDto, ExemptedCourse>()
            //     .ForMember(d => d.CourseType,
            //     op => op.MapFrom(o => EnumStringify.CourseTypeEnumarator(o.CourseType)));

            // CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();

            // CreateMap<Approval, ApprovalDto>().ReverseMap();
        }
    }
}
