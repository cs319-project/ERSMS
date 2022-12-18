using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    /// <summary>A service for CTE (Course Transfer and Exemption) form operations.</summary>
    public class CTEFormService : ICTEFormService
    {
        private readonly ICTEFormRepository _cTEFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly IToDoItemService _toDoItemService;
        private readonly ILoggedCourseService _loggedCourseService;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="CTEFormService"/> class.</summary>
        /// <param name="cTEFormRepository">The repository for CTEForms.</param>
        /// <param name="mapper">The mapper for CTEForms.</param>
        /// <param name="userRepository">The repository for users.</param>
        /// <param name="toDoItemService">The service for to-do items.</param>
        /// <param name="userService">The service for users.</param>
        /// <param name="notificationService">The service for notifications.</param>
        /// <param name="loggedCourseService">The service for logged courses.</param>
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

        /// <summary>Adds a CTE Form to a student.</summary>
        /// <param name="cTEForm">The CTE Form to add.</param>
        /// <returns>True if the CTE Form was added successfully, false otherwise.</returns>
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

        /// <summary>Approves a CTE form by the dean.</summary>
        /// <param name="formId">The ID of the CTE form to approve.</param>
        /// <param name="approval">The approval to approve the form with.</param>
        /// <returns>True if the form was approved successfully, false otherwise.</returns>
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

        /// <summary>Approves a CTE form by the exchange coordinator.</summary>
        /// <param name="formId">The ID of the CTE form to approve.</param>
        /// <param name="approval">The approval to approve the form with.</param>
        /// <returns>True if the form was approved successfully, false otherwise.</returns>
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

        /// <summary>Approve a CTE form by the exchange coordinator.</summary>
        /// <param name="formId">The ID of the CTE form to approve.</param>
        /// <param name="approval">The approval to approve the form with.</param>
        /// <returns>True if the form was approved successfully, false otherwise.</returns>
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

        /// <summary>Deletes a CTEForm.</summary>
        /// <param name="id">The ID of the CTEForm to delete.</param>
        /// <returns>True if the CTEForm was deleted successfully, false otherwise.</returns>
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

        /// <summary>Updates the CTE form.</summary>
        /// <param name="cTEForm">The CTE form to update.</param>
        /// <returns>True if the CTE form was updated successfully, false otherwise.</returns>
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

        /// <summary>Gets the CTE form.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The CTE form.</returns>
        public async Task<CTEFormDto> GetCTEForm(Guid id)
        {
            CTEForm formEntity = await _cTEFormRepository.GetCTEForm(id);
            return _mapper.Map<CTEFormDto>(formEntity);
        }

        /// <summary>Gets the CTE forms of a student.</summary>
        /// <param name="studentID">The student's ID.</param>
        /// <returns>The CTE forms of the student.</returns>
        public async Task<ICollection<CTEFormDto>> GetCTEFormsOfStudent(string studentID)
        {
            Student student = await _userRepository.GetStudentByUserName(studentID);
            ICollection<CTEFormDto> forms = _mapper.Map<ICollection<CTEFormDto>>(student.CTEForms);
            return forms;
        }

        /// <summary>Gets the CTE forms.</summary>
        /// <returns>The CTE forms.</returns>
        public async Task<IEnumerable<CTEFormDto>> GetCTEForms()
        {
            IEnumerable<CTEForm> forms = await _cTEFormRepository.GetCTEForms();
            return _mapper.Map<IEnumerable<CTEFormDto>>(forms);
        }

        /// <summary>Approves the CTE form by the faculty of administration board.</summary>
        /// <param name="formId">The ID of the CTE form to approve.</param>
        /// <param name="approval">The approval to approve the form with.</param>
        /// <returns>True if the CTE form was approved successfully, false otherwise.</returns>
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

        /// <summary>Retrieves the CTE forms by department.</summary>
        /// <param name="userName">The user name of the exchange coordinator.</param>
        /// <returns>The CTE forms by department.</returns>
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

        /// <summary>Gets all archived CTE forms.</summary>
        /// <returns>A collection of CTE forms.</returns>
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

        /// <summary>Gets all non-archived CTE forms.</summary>
        /// <returns>A collection of non-archived CTE forms.</returns>
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

        /// <summary>Retrieves all archived CTE forms by department.</summary>
        /// <param name="userName">The username of the exchange coordinator.</param>
        /// <returns>A collection of CTE forms.</returns>
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

        /// <summary>Retrieves all non-archived CTE forms by department.</summary>
        /// <param name="userName">The username of the exchange coordinator.</param>
        /// <returns>A collection of CTE forms.</returns>
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

        /// <summary>Cancels a CTE form.</summary>
        /// <param name="formId">The CTE form's id.</param>
        /// <returns>True if the form was successfully canceled, false otherwise.</returns>
        public async Task<bool> CancelCTEForm(Guid formId)
        {
            CTEForm formEntity = _cTEFormRepository.GetCTEForm(formId).Result;

            if (CheckIfFormIsOperable(formEntity))
            {
                if (formEntity == null)
                {
                    return false;
                }

                // Delete todo from users
                await _toDoItemService.DeleteToDoItemByCascadeId(formEntity.ToDoItemId);

                formEntity.IsCanceled = true;
                formEntity.IsArchived = true;

                // if (formEntity.ToDoItemId != null)
                // {
                //     // Complete todo
                //     ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                //     await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                // }

                return await _cTEFormRepository.UpdateCTEForm(formEntity);
            }
            return false;
        }

        /// <summary>Archives a CTE form.</summary>
        /// <param name="formId">The ID of the CTE form to archive.</param>
        /// <returns>True if the form was successfully archived, false otherwise.</returns>
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

        /// <summary>Checks if the form is operable.</summary>
        /// <param name="form">The CTE form to check.</param>
        /// <returns>True if the form is operable, false otherwise.</returns>
        private bool CheckIfFormIsOperable(CTEForm form)
        {
            return !form.IsApproved && !form.IsRejected && !form.IsArchived && !form.IsCanceled;
        }

        /// <summary>Logs the accepted course.</summary>
        /// <param name="formDto">The form dto.</param>
        /// <returns>True if the course was logged successfully, false otherwise.</returns>
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

        /// <summary>Uploads a PDF file to the CTEForm</summary>
        /// <param name="formId">The ID of the CTEForm to upload the PDF to.</param>
        /// <param name="pdf">The PDF file.</param>
        /// <returns>Whether the upload was successful.</returns>
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

        /// <summary>Downloads a PDF from the server.</summary>
        /// <param name="formId">The ID of the form whose PDF is to be downloaded.</param>
        /// <returns>A tuple containing the PDF and the file name.</returns>
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

        /// <summary>Retrieves all non-archived CTE forms for the dean.</summary>
        /// <param name="userName">The user name of the dean.</param>
        /// <returns>A collection of CTE forms for the dean.</returns>
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

        /// <summary>Retrieves the archived CTE forms by faculty for the dean.</summary>
        /// <param name="userName">The user name of the dean.</param>
        /// <returns>The archived CTE forms by faculty for the dean.</returns>
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

        /// <summary>Retrieves the non-archived CTE forms for the department chair.</summary>
        /// <param name="userName">The user name of the department chair.</param>
        /// <returns>The non-archived CTE forms for the department chair.</returns>
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


        /// <summary>Retrieves the archived CTE forms for the department chair.</summary>
        /// <param name="userName">The user name of the department chair.</param>
        /// <returns>The archived CTE forms for the department chair.</returns>
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

        /// <summary>Converts a file to a byte array.</summary>
        /// <param name="file">The file.</param>
        /// <returns>Byte array of the file</returns>
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
