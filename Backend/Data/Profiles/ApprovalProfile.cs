using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Approval"/> to <see cref="ApprovalDto"/> and vice versa.</summary>
    public class ApprovalProfile : Profile
    {
        /// <summary>A profile for mapping <see cref="Approval"/> to <see cref="ApprovalDto"/>.</summary>
        public ApprovalProfile()
        {
            CreateMap<Approval, ApprovalDto>().ReverseMap();
        }
    }
}
