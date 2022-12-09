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
        private readonly IToDoItemService _toDoItemService;
        private readonly IMapper _mapper;

        // Constructor
        public PreApprovalFormService(IPreApprovalFormRepository preApprovalFormRepository, IMapper mapper, IUserRepository userRepository, IToDoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;
            _preApprovalFormRepository = preApprovalFormRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            PreApprovalForm formEntity = _preApprovalFormRepository.GetPreApprovalForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.ExchangeCoordinatorApproval = approvalEntity;

            // Complete the ToDoItem
            ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
            await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);

            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }

        public async Task<bool> DeletePreApprovalForm(Guid id)
        {
            PreApprovalForm formEntity = await _preApprovalFormRepository.GetPreApprovalForm(id);

            if (formEntity == null)
                return false;

            // Delete the ToDoItem
            await _toDoItemService.DeleteToDoItemByCascadeId(formEntity.ToDoItemId);
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
            ToDoItemDto todo = new ToDoItemDto();
            todo.Title = "Pre-Approval Form";
            todo.Description = "Review the Pre-Approval Form of " + preApprovalForm.FirstName + " "
                                    + preApprovalForm.LastName + " (" + preApprovalForm.IDNumber + ")"
                                    + " and approve it if it is correct";
            todo.CascadeId = Guid.NewGuid();
            PreApprovalForm formEntity = _mapper.Map<PreApprovalForm>(preApprovalForm);
            formEntity.ToDoItemId = todo.CascadeId;

            await _preApprovalFormRepository.SubmitPreApprovalForm(formEntity);
            return await _toDoItemService.AddToDoItemToAll(todo);
        }

        public async Task<bool> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            PreApprovalForm formEntity = _mapper.Map<PreApprovalForm>(preApprovalForm);

            // Don't update the approval
            PreApprovalForm oldForm = await _preApprovalFormRepository.GetPreApprovalForm(formEntity.Id);
            formEntity.ExchangeCoordinatorApproval = oldForm.ExchangeCoordinatorApproval;

            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }
    }
}
