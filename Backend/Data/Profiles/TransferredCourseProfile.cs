using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="TransferredCourse"/> to <see cref="TransferredCourseDto"/> and vice versa.</summary>
    public class TransferredCourseProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="TransferredCourseDto"/> and <see cref="TransferredCourse"/>.</summary>
        public TransferredCourseProfile()
        {
            CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();
        }
    }
}
