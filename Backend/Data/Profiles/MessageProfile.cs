using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Message"/> to <see cref="MessageDto"/> and vice versa.</summary>
    public class MessageProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="MessageDto"/> and <see cref="Message"/>.</summary>
        public MessageProfile()
        {
            CreateMap<Message, MessageDto>().ReverseMap();
        }
    }
}
