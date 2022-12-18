using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="PlacedStudent"/> to <see cref="PlacedStudentDto"/> and vice versa.</summary>
    public class PlacedStudentProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="PlacedStudentDto"/> and <see cref="PlacedStudent"/>.</summary>
        public PlacedStudentProfile()
        {
            CreateMap<PlacedStudentDto, PlacedStudent>().ReverseMap();
        }
    }
}
