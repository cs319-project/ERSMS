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
    public class DomainUserProfile : Profile
    {
        public DomainUserProfile()
        {
            CreateMap<DomainUserDto, DomainUser>().ReverseMap().ForMember(dest => dest.ActorType, opt => opt.MapFrom(src => EnumStringify.ActorStringify(src.ActorType)));
        }
    }
}
