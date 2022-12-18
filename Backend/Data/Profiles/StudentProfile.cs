using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    /// <summary>A profile for mapping <see cref="Student"/> to <see cref="StudentDto"/> and vice versa.</summary>
    public class StudentProfile : Profile
    {
        /// <summary>Creates a mapping between the <see cref="StudentDto"/> and <see cref="Student"/>.</summary>
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>().ReverseMap();
            CreateMap<SemesterInfo, SemesterInfoDto>().ReverseMap();
            //CreateMap<PreApprovalForm, PreApprovalFormDto>().ReverseMap();
            //CreateMap<ExemptionRequestForm, ExemptionRequestFormDto>().ReverseMap();
            //CreateMap<CTEForm, CTEFormDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();

            //CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();
            //CreateMap<Approval, ApprovalDto>().ReverseMap();
            //CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();
            //CreateMap<ExemptedCourse, ExemptedCourseDto>().ReverseMap();

            //CreateMap<RequestedCourseGroupDto, RequestedCourseGroup>().ReverseMap();
            //CreateMap<RequestedCourse, RequestedCourseDto>().ReverseMap();
        }
    }
}
