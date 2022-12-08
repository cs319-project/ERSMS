using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    public class PreApprovalFormService : IPreApprovalFormService
    {
        private readonly IPreApprovalFormRepository _preApprovalFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Constructor
        public PreApprovalFormService(IPreApprovalFormRepository preApprovalFormRepository, IMapper mapper, IUserRepository userRepository)
        {
            _preApprovalFormRepository = preApprovalFormRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            PreApprovalForm formEntity = _preApprovalFormRepository.GetPreApprovalForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.ExchangeCoordinatorApproval = approvalEntity;

            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }

        public async Task<bool> DeletePreApprovalForm(Guid id)
        {
            return await _preApprovalFormRepository.DeletePreApprovalForm(id);
        }

        public async Task<PreApprovalFormDto> GetPreApprovalForm(Guid id)
        {
            PreApprovalForm formEntity = await _preApprovalFormRepository.GetPreApprovalForm(id);
            return _mapper.Map<PreApprovalFormDto>(formEntity);
        }

        public async Task<IEnumerable<PreApprovalFormDto>> GetPreApprovalForms()
        {
            IEnumerable<PreApprovalForm> formEntities = await _preApprovalFormRepository.GetPreApprovalForms();
            return _mapper.Map<IEnumerable<PreApprovalFormDto>>(formEntities);
        }

        public async Task<ICollection<PreApprovalFormDto>> GetPreApprovalFormsOfStudent(string studentID)
        {
            Student student = await _userRepository.GetStudentByUserName(studentID);
            ICollection<PreApprovalFormDto> forms = _mapper.Map<ICollection<PreApprovalFormDto>>(student.PreApprovalForms);
            return forms;
        }

        public async Task<bool> SubmitPreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            PreApprovalForm formEntity = _mapper.Map<PreApprovalForm>(preApprovalForm);
            return await _preApprovalFormRepository.SubmitPreApprovalForm(formEntity);
        }

        public async Task<bool> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            PreApprovalForm formEntity = _mapper.Map<PreApprovalForm>(preApprovalForm);
            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }
    }
}
