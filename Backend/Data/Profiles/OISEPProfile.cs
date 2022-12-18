using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

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
