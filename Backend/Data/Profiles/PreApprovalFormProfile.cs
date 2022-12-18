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
    /// <summary>A profile for mapping <see cref="PreApprovalForm"/> to <see cref="PreApprovalFormDto"/> and vice versa.</summary>
    public class PreApprovalFormProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="PreApprovalFormDto"/> and <see cref="PreApprovalForm"/>.</summary>
        public PreApprovalFormProfile()
        {
            CreateMap<PreApprovalForm, PreApprovalFormDto>()
            .ForMember(d => d.Semester,
                op => op.MapFrom(o => EnumStringify.SemesterStringify(o.Semester)))
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentStringify(o.Department)));

            CreateMap<PreApprovalFormDto, PreApprovalForm>()
            .ForMember(d => d.Semester,
                op => op.MapFrom(o => EnumStringify.SemesterEnumarator(o.Semester)))
            .ForMember(d => d.Department,
                op => op.MapFrom(o => EnumStringify.DepartmentEnumarator(o.Department)));
        }
    }
}
