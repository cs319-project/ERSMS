using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class MessageProfile : Profile
    {
        // Constructor
        public MessageProfile()
        {
            CreateMap<Entities.Message, DTOs.MessageDto>().ReverseMap();
        }
    }
}
