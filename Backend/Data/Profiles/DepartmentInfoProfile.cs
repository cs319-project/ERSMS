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
    public class DepartmentInfoProfile : Profile
    {
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
