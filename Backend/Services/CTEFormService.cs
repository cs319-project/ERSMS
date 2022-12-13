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
                Student student = await _userService.GetStudent(cTEForm.IDNumber);
                if (student != null)
                    flag = await _toDoItemService.AddToDoItemToAllByDepartment(todo, student.Major.DepartmentName);
            }

            return flag;
        }

        public async Task<bool> ApproveFormDean(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.DeanApproval = approvalEntity;

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }
                else if (formEntity.ChairApproval != null
                            && formEntity.ExchangeCoordinatorApproval != null
                            && formEntity.FacultyOfAdministrationBoardApproval != null
                            && formEntity.ChairApproval.IsApproved
                            && formEntity.ExchangeCoordinatorApproval.IsApproved
                            && formEntity.FacultyOfAdministrationBoardApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ApproveFormChair(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.ChairApproval = approvalEntity;

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }
                else if (formEntity.DeanApproval != null
                            && formEntity.ExchangeCoordinatorApproval != null
                            && formEntity.FacultyOfAdministrationBoardApproval != null
                            && formEntity.DeanApproval.IsApproved
                            && formEntity.ExchangeCoordinatorApproval.IsApproved
                            && formEntity.FacultyOfAdministrationBoardApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.ExchangeCoordinatorApproval = approvalEntity;

                if (formEntity.ToDoItemId != null)
                {
                    // Complete todo
                    ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                    await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                }

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;
                }
                else if (formEntity.DeanApproval != null
                            && formEntity.ChairApproval != null
                            && formEntity.FacultyOfAdministrationBoardApproval != null
                            && formEntity.DeanApproval.IsApproved
                            && formEntity.ChairApproval.IsApproved
                            && formEntity.FacultyOfAdministrationBoardApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
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

            if (CheckIfFormIsOperable(formEntity))
            {
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
            return false;
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
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.FacultyOfAdministrationBoardApproval = approvalEntity;

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }
                else if (formEntity.DeanApproval != null
                            && formEntity.ExchangeCoordinatorApproval != null
                            && formEntity.ChairApproval != null
                            && formEntity.DeanApproval.IsApproved
                            && formEntity.ExchangeCoordinatorApproval.IsApproved
                            && formEntity.ChairApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete todo
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<ICollection<CTEFormDto>> GetCTEFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
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

        public async Task<ICollection<CTEFormDto>> GetArchivedCTEForms()
        {
            IEnumerable<CTEForm> forms = await _cTEFormRepository.GetCTEForms();
            ICollection<CTEFormDto> formsToReturn = new List<CTEFormDto>();

            foreach (CTEForm form in forms)
            {
                if (form.IsArchived)
                {
                    formsToReturn.Add(_mapper.Map<CTEFormDto>(form));
                }
            }

            return formsToReturn;
        }

        public async Task<ICollection<CTEFormDto>> GetNonArchivedCTEForms()
        {
            IEnumerable<CTEForm> forms = await _cTEFormRepository.GetCTEForms();
            ICollection<CTEFormDto> formsToReturn = new List<CTEFormDto>();

            foreach (CTEForm form in forms)
            {
                if (!form.IsArchived)
                {
                    formsToReturn.Add(_mapper.Map<CTEFormDto>(form));
                }
            }

            return formsToReturn;
        }

        public async Task<ICollection<CTEFormDto>> GetArchivedCTEFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            ICollection<CTEFormDto> forms = await GetArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != coordinator.Department.DepartmentName)
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }

        public async Task<ICollection<CTEFormDto>> GetNonArchivedCTEFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            ICollection<CTEFormDto> forms = await GetNonArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != coordinator.Department.DepartmentName)
                {
                    forms.Remove(form);
                }
            }
            return forms;
        }

        public async Task<bool> CancelCTEForm(Guid formId)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;

            if (CheckIfFormIsOperable(formEntity))
            {
                if (formEntity == null)
                {
                    return false;
                }

                formEntity.IsCanceled = true;
                formEntity.IsArchived = true;

                if (formEntity.ToDoItemId != null)
                {
                    // Complete todo
                    ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                    await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ArchiveCTEForm(Guid formId)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;

            if (formEntity == null)
            {
                return false;
            }

            if (CheckIfFormIsOperable(formEntity))
            {
                formEntity.IsArchived = true;

                if (formEntity.ToDoItemId != null)
                {
                    // Complete todo
                    ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                    await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        private bool CheckIfFormIsOperable(CTEForm form)
        {
            return !form.IsApproved && !form.IsRejected && !form.IsArchived && !form.IsCanceled;
        }
    }
}