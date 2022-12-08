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

        public async Task<bool> AddCTEFormToStudent(CTEFormDto cTEForm)
        {
            CTEForm formEntity = _mapper.Map<CTEForm>(cTEForm);
            bool flag = await _cTEFormRepository.AddCTEFormToStudent(cTEForm.IDNumber, formEntity);

            return flag;
        }

        public async Task<bool> ApproveFormDean(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.DeanApproval = approvalEntity;

            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<bool> ApproveFormChair(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.ChairApproval = approvalEntity;

            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.ExchangeCoordinatorApproval = approvalEntity;

            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<bool> DeleteCTEForm(Guid id)
        {
            return await _cTEFormRepository.DeleteCTEForm(id);
        }

        public async Task<bool> UpdateCTEForm(CTEFormDto cTEForm)
        {
            CTEForm formEntity = _mapper.Map<CTEForm>(cTEForm);
            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<CTEFormDto> GetCTEForm(Guid id)
        {
            CTEForm formEntity = await _cTEFormRepository.GetCTEForm(id);
            return _mapper.Map<CTEFormDto>(formEntity);
        }

        public async Task<ICollection<CTEFormDto>> GetCTEFormsOfStudent(string studentID)
        {
            Student student = await _userRepository.GetStudentByUserName(studentID);
            ICollection<CTEFormDto> forms = _mapper.Map<ICollection<CTEFormDto>>(student.CTEForms);
            return forms;
        }

        public async Task<IEnumerable<CTEFormDto>> GetCTEForms()
        {
            IEnumerable<CTEForm> forms = await _cTEFormRepository.GetCTEForms();
            return _mapper.Map<IEnumerable<CTEFormDto>>(forms);
        }
    }
}
