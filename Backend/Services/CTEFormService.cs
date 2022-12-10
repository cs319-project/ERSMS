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
        private readonly IToDoItemService _toDoItemService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public CTEFormService(ICTEFormRepository cTEFormRepository, IMapper mapper,
                                IUserRepository userRepository, IToDoItemService toDoItemService, IUserService userService)
        {
            _toDoItemService = toDoItemService;
            _userService = userService;
            _cTEFormRepository = cTEFormRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddCTEFormToStudent(CTEFormDto cTEForm)
        {
            ToDoItemDto todo = new ToDoItemDto();
            todo.CascadeId = Guid.NewGuid();
            todo.Title = "Review CTE Form";
            todo.Description = "Review the CTE Form of " + cTEForm.FirstName + " "
                                    + cTEForm.LastName + " (" + cTEForm.IDNumber + ")"
                                    + " and approve or reject it.";

            CTEForm formEntity = _mapper.Map<CTEForm>(cTEForm);
            formEntity.ToDoItemId = todo.CascadeId;
            bool flag = await _cTEFormRepository.AddCTEFormToStudent(cTEForm.IDNumber, formEntity);

            if (flag)
            {
                flag = await _toDoItemService.AddToDoItemToAll(todo);
            }

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

            // Complete todo
            ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
            await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);

            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<bool> DeleteCTEForm(Guid id)
        {
            CTEForm form = await _cTEFormRepository.GetCTEForm(id);

            if (form == null)
            {
                return false;
            }

            await _toDoItemService.DeleteToDoItemByCascadeId(form.ToDoItemId);
            return await _cTEFormRepository.DeleteCTEForm(id);
        }

        public async Task<bool> UpdateCTEForm(CTEFormDto cTEForm)
        {
            CTEForm formEntity = _mapper.Map<CTEForm>(cTEForm);

            CTEForm oldForm = await _cTEFormRepository.GetCTEForm(formEntity.Id);
            if (oldForm == null)
            {
                return false;
            }

            // Don't allow the approval to be updated
            formEntity.DeanApproval = oldForm.DeanApproval;
            formEntity.ChairApproval = oldForm.ChairApproval;
            formEntity.ExchangeCoordinatorApproval = oldForm.ExchangeCoordinatorApproval;
            formEntity.FacultyOfAdministrationBoardApproval = oldForm.FacultyOfAdministrationBoardApproval;

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

        public async Task<bool> ApproveFacultyOfAdministrationBoard(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            formEntity.FacultyOfAdministrationBoardApproval = approvalEntity;

            return await _cTEFormRepository.UpdateCTEForm(formEntity);
        }

        public async Task<ICollection<CTEFormDto>> GetCTEFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetCoordinator(userName);
            IEnumerable<CTEFormDto> forms = await GetCTEForms();
            ICollection<CTEFormDto> formsToReturn = new List<CTEFormDto>();

            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName == coordinator.Department.DepartmentName)
                {
                    formsToReturn.Add(form);
                }
            }

            return formsToReturn;
        }
    }
}
