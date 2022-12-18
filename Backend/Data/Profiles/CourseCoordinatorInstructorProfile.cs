using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="CourseCoordinatorInstructor"/> to <see cref="CourseCoordinatorInstructorDto"/> and vice versa.</summary>
    public class CourseCoordinatorInstructorProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="CourseCoordinatorInstructorDto"/> and <see cref="CourseCoordinatorInstructor"/>.</summary>
        public CourseCoordinatorInstructorProfile()
        {
            CreateMap<CourseCoordinatorInstructorDto, CourseCoordinatorInstructor>().ReverseMap();
        }
    }
}
