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
    public class RequestedCourseGroupProfile : Profile
    {
        //Constructor
        public RequestedCourseGroupProfile()
        {
            CreateMap<RequestedCourseGroup, RequestedCourseGroupDto>().ReverseMap();
        }
    }
}