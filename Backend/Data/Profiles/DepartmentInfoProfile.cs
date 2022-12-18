using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Entities;
using Backend.DTOs;
using Backend.Utilities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="DepartmentInfo"/> to <see cref="DepartmentInfoDto"/> and vice versa.</summary>
    public class DepartmentInfoProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="DepartmentInfoDto"/> and <see cref="DepartmentInfo"/>.</summary>
        public DepartmentInfoProfile()
        {
            CreateMap<DepartmentInfo, DepartmentInfoDto>()
               .ForMember(d => d.DepartmentName,
               op => op.MapFrom(o => EnumStringify.DepartmentStringify(o.DepartmentName)));

            CreateMap<DepartmentInfoDto, DepartmentInfo>()
               .ForMember(d => d.DepartmentName,
               op => op.MapFrom(o => EnumStringify.DepartmentEnumarator(o.DepartmentName)));
        }
    }
}
