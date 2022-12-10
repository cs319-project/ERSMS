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
        private readonly IUserService _userService;
        private readonly IToDoItemService _toDoItemService;
        private readonly IMapper _mapper;

        // Constructor
        public PreApprovalFormService(IPreApprovalFormRepository preApprovalFormRepository,
                                        IMapper mapper, IUserRepository userRepository,
                                        IToDoItemService toDoItemService, IUserService userService)
        {
            _toDoItemService = toDoItemService;
            _userService = userService;
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

        public async Task<bool> ApproveFormFacultyAdministrationBoard(Guid formId, ApprovalDto approval)
        {
            PreApprovalForm formEntity = _preApprovalFormRepository.GetPreApprovalForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.FacultyAdministrationBoardApproval = approvalEntity;

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

        public async Task<ICollection<PreApprovalFormDto>> GetPreApprovalFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            IEnumerable<PreApprovalFormDto> forms = await GetPreApprovalForms();
            ICollection<PreApprovalFormDto> listToReturn = new List<PreApprovalFormDto>();

            foreach (PreApprovalFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName == coordinator.Department.DepartmentName)
                {
                    listToReturn.Add(form);
                }
            }

            return listToReturn;
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
            todo.Title = "Review Pre-Approval Form";
            todo.Description = "Review the Pre-Approval Form of " + preApprovalForm.FirstName + " "
                                    + preApprovalForm.LastName + " (" + preApprovalForm.IDNumber + ")"
                                    + " and approve or reject it.";
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
            formEntity.FacultyAdministrationBoardApproval = oldForm.FacultyAdministrationBoardApproval;

            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }

        public async Task<bool> CancelPreApprovalForm(Guid id)
        {
            PreApprovalForm formEntity = await _preApprovalFormRepository.GetPreApprovalForm(id);

            if (formEntity == null)
                return false;

            // Delete the ToDoItem
            await _toDoItemService.DeleteToDoItemByCascadeId(formEntity.ToDoItemId);

            formEntity.IsCanceled = true;
            return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
        }
    }
}
