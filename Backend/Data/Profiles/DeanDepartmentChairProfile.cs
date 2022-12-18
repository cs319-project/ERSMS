using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="DeanDepartmentChair"/> to <see cref="DeanDepartmentChairDto"/> and vice versa.</summary>
    public class DeanDepartmentChairProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="DeanDepartmentChairDto"/> and <see cref="DeanDepartmentChair"/>.</summary>
        public DeanDepartmentChairProfile()
        {
            CreateMap<DeanDepartmentChairDto, DeanDepartmentChair>().ReverseMap();
        }
    }
}
