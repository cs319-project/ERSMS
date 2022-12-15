using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    public class EquivalenceRequestProfile : Profile
    {
        // Constructor
        public EquivalenceRequestProfile()
        {
            CreateMap<EquivalenceRequest, EquivalenceRequestDto>().ReverseMap();
        }
    }
}
