using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="PlacementTable"/> to <see cref="PlacementTableDto"/> and vice versa.</summary>
    public class PlacementTableProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="PlacementTableDto"/> and <see cref="PlacementTable"/>.</summary>
        public PlacementTableProfile()
        {
            CreateMap<PlacementTableDto, PlacementTable>().ReverseMap();
        }
    }
}
