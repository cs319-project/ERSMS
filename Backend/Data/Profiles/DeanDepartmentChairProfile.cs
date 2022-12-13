using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.DTOs;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class DeanDepartmentChairProfile : Profile
    {
        public DeanDepartmentChairProfile()
        {
            CreateMap<DeanDepartmentChairDto, DeanDepartmentChair>().ReverseMap().ForMember(dest => dest.ActorType, opt => opt.MapFrom(src => "Dean Department Chair"));
        }
    }
}
