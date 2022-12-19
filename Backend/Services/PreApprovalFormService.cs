using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class PreApprovalFormService : IPreApprovalFormService
    {
        private readonly IPreApprovalFormRepository _preApprovalFormRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly IToDoItemService _toDoItemService;
        private readonly IMapper _mapper;

        // Constructor
        public PreApprovalFormService(IPreApprovalFormRepository preApprovalFormRepository,
                                        IMapper mapper, IUserRepository userRepository,
                                        IToDoItemService toDoItemService, IUserService userService,
                                        INotificationService notificationService)
        {
            _toDoItemService = toDoItemService;
            _notificationService = notificationService;
            _userService = userService;
            _preApprovalFormRepository = preApprovalFormRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval)
        {
            PreApprovalForm formEntity = _preApprovalFormRepository.GetPreApprovalForm(formId).Result;
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.ExchangeCoordinatorApproval = approvalEntity;

                if (formEntity.ToDoItemId != null)
                {
                    // Complete the ToDoItem
                    ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                    await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                }

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;
                }
                else if (formEntity.FacultyAdministrationBoardApproval != null
                            && formEntity.FacultyAdministrationBoardApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;
                }

                await _notificationService.CreateNewApprovalNotification(formEntity, FormType.PreApprovalForm,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
                return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ApproveFormFacultyAdministrationBoard(Guid formId, ApprovalDto approval)
        {
            PreApprovalForm formEntity = _preApprovalFormRepository.GetPreApprovalForm(formId).Result;
            if (CheckIfFormIsOperable(formEntity))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                formEntity.FacultyAdministrationBoardApproval = approvalEntity;

                if (!approvalEntity.IsApproved)
                {
                    formEntity.IsRejected = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete the ToDoItem
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }
                else if (formEntity.ExchangeCoordinatorApproval != null
                            && formEntity.ExchangeCoordinatorApproval.IsApproved)
                {
                    formEntity.IsApproved = true;
                    formEntity.IsArchived = true;

                    if (formEntity.ToDoItemId != null)
                    {
                        // Complete the ToDoItem
                        ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                        await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                    }
                }

                await _notificationService.CreateNewApprovalNotification(formEntity, FormType.PreApprovalForm,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
                return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
            }
            return false;
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

            var flag = await _preApprovalFormRepository.SubmitPreApprovalForm(formEntity);

            if (flag)
            {
                Student student = await _userRepository.GetStudentByUserName(preApprovalForm.IDNumber);
                if (student != null)
                    await _toDoItemService.AddToDoItemToAllByDepartment(todo, student.Major.DepartmentName);

                await _notificationService.CreateNewFormNotification(formEntity, FormType.PreApprovalForm);
            }

            return flag;
        }

        public async Task<bool> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm)
        {
            PreApprovalForm formEntity = _mapper.Map<PreApprovalForm>(preApprovalForm);
            PreApprovalForm oldForm = await _preApprovalFormRepository.GetPreApprovalForm(formEntity.Id);
            if (CheckIfFormIsOperable(oldForm))
            {
                // Don't update the approval
                formEntity.ExchangeCoordinatorApproval = oldForm.ExchangeCoordinatorApproval;
                formEntity.FacultyAdministrationBoardApproval = oldForm.FacultyAdministrationBoardApproval;
                formEntity.IsApproved = oldForm.IsApproved;
                formEntity.IsCanceled = oldForm.IsCanceled;
                formEntity.IsArchived = oldForm.IsArchived;
                formEntity.IsRejected = oldForm.IsRejected;
                return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
            }
            return false;
        }

        public async Task<bool> CancelPreApprovalForm(Guid id)
        {
            PreApprovalForm formEntity = await _preApprovalFormRepository.GetPreApprovalForm(id);

            if (formEntity == null)
                return false;

            if (CheckIfFormIsOperable(formEntity))
            {
                // Delete the ToDoItem
                await _toDoItemService.DeleteToDoItemByCascadeId(formEntity.ToDoItemId);

                formEntity.IsCanceled = true;
                formEntity.IsArchived = true;

                return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
            }
            return false;
        }

        public async Task<bool> ArchivePreApprovalForm(Guid formId)
        {
            PreApprovalForm formEntity = await _preApprovalFormRepository.GetPreApprovalForm(formId);

            if (formEntity == null)
                return false;

            if (CheckIfFormIsOperable(formEntity))
            {
                formEntity.IsArchived = true;

                if (formEntity.ToDoItemId != null)
                {
                    // Complete the ToDoItem
                    ToDoItemDto todo = await _toDoItemService.GetToDoItemByCascadeId(formEntity.ToDoItemId);
                    await _toDoItemService.ChangeCompleteToDoItem(todo.Id, true);
                }
                return await _preApprovalFormRepository.UpdatePreApprovalForm(formEntity);
            }
            return false;
        }

        public async Task<ICollection<PreApprovalFormDto>> GetArchivedPreApprovalForms()
        {
            IEnumerable<PreApprovalForm> formEntities = await _preApprovalFormRepository.GetPreApprovalForms();
            ICollection<PreApprovalFormDto> listToReturn = new List<PreApprovalFormDto>();

            foreach (PreApprovalForm form in formEntities)
            {
                if (form.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<PreApprovalFormDto>(form));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<PreApprovalFormDto>> GetNonArchivedPreApprovalForms()
        {
            IEnumerable<PreApprovalForm> formEntities = await _preApprovalFormRepository.GetPreApprovalForms();
            ICollection<PreApprovalFormDto> listToReturn = new List<PreApprovalFormDto>();

            foreach (PreApprovalForm form in formEntities)
            {
                if (!form.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<PreApprovalFormDto>(form));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<PreApprovalFormDto>> GetArchivedPreApprovalFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            ICollection<PreApprovalFormDto> forms = await GetArchivedPreApprovalForms();
            foreach (PreApprovalFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != coordinator.Department.DepartmentName)
                {
                    forms.Remove(form);
                }
            }

            return forms;
        }

        public async Task<ICollection<PreApprovalFormDto>> GetNonArchivedPreApprovalFormsByDepartment(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            ICollection<PreApprovalFormDto> forms = await GetNonArchivedPreApprovalForms();
            foreach (PreApprovalFormDto form in forms)
            {
                var student = await _userRepository.GetStudentByUserName(form.IDNumber);
                if (student.Major.DepartmentName != coordinator.Department.DepartmentName)
                {
                    forms.Remove(form);
                }
            }

            return forms;
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
                return await _preApprovalFormRepository.UploadPdf(formId, pdfBytes, pdf.FileName);
            }

        }

        public async Task<(byte[], string)> DownloadPdf(Guid formId)
        {
            PreApprovalForm form = await _preApprovalFormRepository.GetPreApprovalForm(formId);

            if (form != null)
            {
                return (form.PDF, form.FileName);
            }
            else
            {
                return (null, null);
            }
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

        private bool CheckIfFormIsOperable(PreApprovalForm form)
        {
            return !form.IsApproved && !form.IsRejected && !form.IsArchived && !form.IsCanceled;
        }

        public async Task<bool> DeletePdf(Guid formId)
        {
            var form = await _preApprovalFormRepository.GetPreApprovalForm(formId);

            if (form == null || form.PDF == null || form.PDF.Length == 0)
            {
                return false;
            }

            form.PDF = new byte[0];
            form.FileName = string.Empty;
            return await _preApprovalFormRepository.UpdatePreApprovalForm(form);
        }
    }
}
