using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    public class TransferredCourseGroupProfile : Profile
    {
        // Constructor
        public TransferredCourseGroupProfile()
        {
            CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();
        }
    }
}
