using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="EquivalenceRequest"/> to <see cref="EquivalenceRequestDto"/> and vice versa.</summary>
    public class EquivalenceRequestProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="EquivalenceRequestDto"/> and <see cref="EquivalenceRequest"/>.</summary>
        public EquivalenceRequestProfile()
        {
            CreateMap<EquivalenceRequest, EquivalenceRequestDto>().ReverseMap();
        }
    }
}
