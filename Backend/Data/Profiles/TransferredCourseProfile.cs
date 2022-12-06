using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    public class TransferredCourseProfile : Profile
    {
        // Constructor
        public TransferredCourseProfile()
        {
            CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();
        }
    }
}
