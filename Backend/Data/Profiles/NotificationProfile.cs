using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Notification"/> to <see cref="NotificationDto"/> and vice versa.</summary>
    public class NotificationProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="NotificationDto"/> and <see cref="Notification"/>.</summary>
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>().ReverseMap();
        }
    }
}
