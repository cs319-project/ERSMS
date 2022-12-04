using System;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;

namespace Backend.Data.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, Student>().ReverseMap().ForMember(dest => dest.ActorType, opt => opt.MapFrom(src => "Student"));
            CreateMap<SemesterInfo, SemesterInfoDto>().ReverseMap();
            CreateMap<DepartmentInfo, DepartmentInfoDto>().ReverseMap();
            CreateMap<PreApprovalForm, PreApprovalFormDto>().ReverseMap();
            CreateMap<ExemptionRequestForm, ExemptionRequestFormDto>().ReverseMap();
            CreateMap<CTEForm, CTEFormDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<DomainUserDto, DomainUser>().ReverseMap();

            CreateMap<TransferredCourseGroup, TransferredCourseGroupDto>().ReverseMap();
            CreateMap<Approval, ApprovalDto>().ReverseMap();
            CreateMap<TransferredCourseDto, TransferredCourse>().ReverseMap();
            CreateMap<ExemptedCourse, ExemptedCourseDto>().ReverseMap();

            CreateMap<RequestedCourseGroupDto, RequestedCourseGroup>().ReverseMap();
            CreateMap<RequestedCourse, RequestedCourseDto>().ReverseMap();
            CreateMap<RequestedExemptedCourse, RequestedExemptedCourseDto>().ReverseMap();
        }
    }
}
