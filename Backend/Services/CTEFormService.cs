using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class CTEFormService : ICTEFormService
    {
        private readonly ICTEFormRepository _cTEFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IToDoItemService _toDoItemService;
        private readonly ILoggedCourseService _loggedCourseService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public CTEFormService(ICTEFormRepository cTEFormRepository, IMapper mapper,
                                IUserRepository userRepository, IToDoItemService toDoItemService,
                                IUserService userService, INotificationService notificationService,
                                ILoggedCourseService loggedCourseService)
        {
            _toDoItemService = toDoItemService;
            _notificationService = notificationService;
            _loggedCourseService = loggedCourseService;
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
                await _notificationService.CreateNewFormNotification(formEntity, FormType.CTEForm);
            }

            return flag;
        }

        public async Task<bool> ApproveFormDean(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = await _cTEFormRepository.GetCTEForm(formId);
            CTEFormDto formDto = _mapper.Map<CTEFormDto>(formEntity);
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

                    // log the transferred courses
                    await LogTheAcceptedCourse(formDto);
                }

                // send notification
                await _notificationService.CreateNewApprovalNotification(formEntity, FormType.CTEForm,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ApproveFormChair(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            CTEFormDto formDto = _mapper.Map<CTEFormDto>(formEntity);
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

                    // log the transferred courses
                    await LogTheAcceptedCourse(formDto);
                }

                // send notification
                await _notificationService.CreateNewApprovalNotification(formEntity, FormType.CTEForm,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;
            CTEFormDto formDto = _mapper.Map<CTEFormDto>(formEntity);
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

                    // log the transferred courses
                    await LogTheAcceptedCourse(formDto);
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
                formEntity.IsApproved = oldForm.IsApproved;
                formEntity.IsRejected = oldForm.IsRejected;
                formEntity.IsArchived = oldForm.IsArchived;
                formEntity.IsCanceled = oldForm.IsCanceled;
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
            CTEFormDto formDto = _mapper.Map<CTEFormDto>(formEntity);
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
                        // Complete todo of coordinator
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }

                    // log the transferred courses
                    await LogTheAcceptedCourse(formDto);
                }

                await _notificationService.CreateNewApprovalNotification(formEntity, FormType.CTEForm,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
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

        private async Task<bool> LogTheAcceptedCourse(CTEFormDto formDto)
        {
            // Save the form to the logging database
            var form = new LoggedTransferredCourseDto
            {
                TransferredCourseGroups = formDto.TransferredCourseGroups
            };

            foreach (TransferredCourseGroupDto group in form.TransferredCourseGroups)
            {
                group.Id = Guid.NewGuid();
                group.ExemptedCourse.Id = Guid.NewGuid();

                foreach (TransferredCourseDto course in group.TransferredCourses)
                {
                    course.Id = Guid.NewGuid();
                }
            }

            return await _loggedCourseService.CreateLoggedTransferredCourse(form);
        }

        public async Task<bool> UploadPdf(Guid formId, IFormFile pdf)
        {
            if (Path.GetExtension(pdf.FileName) != ".pdf")
            {
                return false;
            }
            else
            {
                byte[] pdfBytes = await SaveFile(pdf);
                return await _cTEFormRepository.UploadPdf(formId, pdfBytes, pdf.FileName);
            }

        }

        public async Task<(byte[], string)> DownloadPdf(Guid formId)
        {
            CTEForm form = await _cTEFormRepository.GetCTEForm(formId);

            if (form != null)
            {
                return (form.PDF, form.FileName);
            }
            else
            {
                return (null, null);
            }
        }

        public async Task<ICollection<CTEFormDto>> GetNonArchivedCTEFormsByFacultyForDean(string userName)
        {
            DeanDepartmentChairDto dean = await _userService.GetDean(userName);
            if (dean == null)
            {
                return null;
            }
            ICollection<CTEFormDto> forms = await GetNonArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.FacultyName != EnumStringify.FacultyEnumarator(dean.Department.FacultyName))
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }

        public async Task<ICollection<CTEFormDto>> GetArchivedCTEFormsByFacultyForDean(string userName)
        {
            DeanDepartmentChairDto dean = await _userService.GetDean(userName);
            if (dean == null)
            {
                return null;
            }
            ICollection<CTEFormDto> forms = await GetArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.FacultyName != EnumStringify.FacultyEnumarator(dean.Department.FacultyName))
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }

        public async Task<ICollection<CTEFormDto>> GetNonArchivedCTEFormsByDepartmentForChair(string userName)
        {
            DeanDepartmentChairDto departmentChair = await _userService.GetDepartmentChair(userName);
            if (departmentChair == null)
            {
                return null;
            }
            ICollection<CTEFormDto> forms = await GetNonArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != EnumStringify.DepartmentEnumarator(departmentChair.Department.DepartmentName))
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }


        public async Task<ICollection<CTEFormDto>> GetArchivedCTEFormsByDepartmentForChair(string userName)
        {
            DeanDepartmentChairDto departmentChair = await _userService.GetDepartmentChair(userName);
            if (departmentChair == null)
            {
                return null;
            }
            ICollection<CTEFormDto> forms = await GetArchivedCTEForms();
            foreach (CTEFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != EnumStringify.DepartmentEnumarator(departmentChair.Department.DepartmentName))
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }

        private async Task<byte[]> SaveFile(IFormFile file)
        {
            // convert file to byte array
            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }
    }
}
