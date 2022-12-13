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
    public class PreApprovalFormProfile : Profile
    {
        //Constructor
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
