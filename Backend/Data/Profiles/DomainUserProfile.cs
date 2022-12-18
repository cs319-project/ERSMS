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
    /// <summary>A profile for mapping <see cref="DomainUser"/> to <see cref="DomainUserDto"/> and vice versa.</summary>
    public class DomainUserProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="DomainUserDto"/> and <see cref="DomainUser"/>.</summary>
        public DomainUserProfile()
        {
            CreateMap<DomainUserDto, DomainUser>().ReverseMap()
                .ForMember(dest => dest.ActorType, opt => opt.MapFrom(src => EnumStringify.ActorStringify(src.ActorType)));
        }
    }
}
