using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<AdminDto, Admin>().ReverseMap().ForMember(dest => dest.ActorType, opt => opt.MapFrom(src => "Admin"));
        }
    }
}
