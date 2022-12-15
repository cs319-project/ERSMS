using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class OISEPProfile : Profile
    {
        public OISEPProfile()
        {
            CreateMap<OISEPDto, OISEP>().ReverseMap();
        }
    }
}
