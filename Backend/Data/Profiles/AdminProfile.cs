using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using AutoMapper;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="AdminDto"/> to <see cref="Admin"/> and vice versa.</summary>
    public class AdminProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="AdminDto"/> and <see cref="Admin"/>.</summary>
        public AdminProfile()
        {
            CreateMap<AdminDto, Admin>().ReverseMap();
        }
    }
}
