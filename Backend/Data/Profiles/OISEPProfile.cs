using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using AutoMapper;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="OISEP"/> to <see cref="OISEPDto"/> and vice versa.</summary>
    public class OISEPProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="OISEPDto"/> and <see cref="OISEP"/>.</summary>
        public OISEPProfile()
        {
            CreateMap<OISEPDto, OISEP>().ReverseMap();
        }
    }
}
