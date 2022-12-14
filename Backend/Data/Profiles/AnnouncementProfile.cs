using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class AnnouncementProfile : Profile
    {
        // Constructor
        public AnnouncementProfile()
        {
            // Source -> Target
            CreateMap<Entities.Announcement, DTOs.AnnouncementDto>().ReverseMap();
        }
    }
}
