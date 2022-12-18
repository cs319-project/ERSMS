using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Announcement"/> to <see cref="AnnouncementDto"/> and vice versa.</summary>
    public class AnnouncementProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="AnnouncementDto"/> and <see cref="Announcement"/>.</summary>
        public AnnouncementProfile()
        {
            CreateMap<Announcement, AnnouncementDto>().ReverseMap();
        }
    }
}
