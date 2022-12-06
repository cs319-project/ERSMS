using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;

namespace Backend.Services
{
    public class CTEFormService : ICTEFormService
    {
        private readonly ICTEFormRepository _cTEFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Constructor
        public CTEFormService(ICTEFormRepository cTEFormRepository, IMapper mapper, IUserRepository userRepository)
        {
            _cTEFormRepository = cTEFormRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddCTEFormToStudent(string userName, CTEFormDto cTEForm)
        {
            CTEForm formEntity = _mapper.Map<CTEForm>(cTEForm);
            var student = await _userRepository.GetStudentByUserName(userName);
            formEntity.FirstName = student.FirstName;
            formEntity.LastName = student.LastName;
            formEntity.IDNumber = student.IdentityUser.UserName;
            formEntity.Department = student.Majors.FirstOrDefault().DepartmentName;
            formEntity.HostUniversityName = student.ExchangeSchool;
            bool flag = await _cTEFormRepository.AddCTEFormToStudent(userName, formEntity);

            return flag;
        }

        public Task<bool> DeleteCTEForm(Guid id)
        {
            return _cTEFormRepository.DeleteCTEForm(id);
        }
    }
}
