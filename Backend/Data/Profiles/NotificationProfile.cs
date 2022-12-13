using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Backend.Data.Profiles
{
    public class NotificationProfile : Profile
    {
        // Constructor
        public NotificationProfile()
        {
            CreateMap<Entities.Notification, DTOs.NotificationDto>().ReverseMap();
        }
    }
}
