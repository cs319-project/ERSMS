using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class EquivalenceRequestService : IEquivalenceRequestService
    {
        private readonly IEquivalenceRequestRepository _equivalenceRequestRepository;
        private readonly INotificationService _notificationService;
        private readonly ILoggedCourseService _loggedCourseService;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public EquivalenceRequestService(IEquivalenceRequestRepository equivalenceRequestRepository,
                                            IUserRepository userRepository, IUserService userService, IMapper mapper,
                                            INotificationService notificationService, ILoggedCourseService loggedCourseService)
        {
            _equivalenceRequestRepository = equivalenceRequestRepository;
            _userRepository = userRepository;
            _loggedCourseService = loggedCourseService;
            _notificationService = notificationService;
            _mapper = mapper;
            _userService = userService;
        }

        // Method
        public async Task<bool> AddEquivalenceRequestToStudent(EquivalenceRequestDto equivalenceRequest, IFormFile file)
        {
            EquivalenceRequest request = _mapper.Map<EquivalenceRequest>(equivalenceRequest);
            request.Syllabus = await SaveFile(file);
            var flag = await _equivalenceRequestRepository.AddEquivalenceRequestToStudent(equivalenceRequest.StudentId, request);

            if (flag)
            {
                await _notificationService.CreateNewFormNotification(request, FormType.EquivalanceRequest);
            }

            return flag;
        }

        public async Task<IEnumerable<EquivalenceRequestDto>> GetEquivalenceRequests()
        {
            return _mapper.Map<IEnumerable<EquivalenceRequestDto>>(await _equivalenceRequestRepository.GetEquivalenceRequests());
        }

        public async Task<bool> DeleteEquivalenceRequest(Guid id)
        {
            return await _equivalenceRequestRepository.DeleteEquivalenceRequest(id);
        }

        public async Task<EquivalenceRequestDto> GetEquivalenceRequest(Guid id)
        {
            return _mapper.Map<EquivalenceRequestDto>(await _equivalenceRequestRepository.GetEquivalenceRequest(id));
        }

        public async Task<bool> UpdateEquivalenceRequest(EquivalenceRequestDto equivalenceRequest)
        {
            var oldRequest = await _equivalenceRequestRepository.GetEquivalenceRequest(equivalenceRequest.Id);
            if (CheckIfRequestIsOperable(oldRequest))
            {
                EquivalenceRequest request = _mapper.Map<EquivalenceRequest>(equivalenceRequest);
                request.InstructorApproval = oldRequest.InstructorApproval;
                request.IsCanceled = oldRequest.IsCanceled;
                request.IsRejected = oldRequest.IsRejected;
                request.IsApproved = oldRequest.IsApproved;
                request.IsArchived = oldRequest.IsArchived;
                request.FileName = oldRequest.FileName;
                return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
            }
            return false;
        }

        public async Task<bool> UpdateEquivalenceRequestSyllabus(Guid id, IFormFile file)
        {
            EquivalenceRequest request = await _equivalenceRequestRepository.GetEquivalenceRequest(id);
            request.Syllabus = await SaveFile(file);
            request.FileName = file.FileName;
            return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestsOfStudent(string studentID)
        {
            Student student = await _userRepository.GetStudentByUserName(studentID);
            ICollection<EquivalenceRequestDto> equivalanceRequests = _mapper.Map<ICollection<EquivalenceRequestDto>>(student.EquivalanceRequestForms);
            return equivalanceRequests;
        }

        public async Task<bool> ApproveRequest(Guid requestId, ApprovalDto approval)
        {
            EquivalenceRequest request = await _equivalenceRequestRepository.GetEquivalenceRequest(requestId);
            EquivalenceRequestDto requestDto = _mapper.Map<EquivalenceRequestDto>(request);
            if (request == null)
            {
                return false;
            }
            if (CheckIfRequestIsOperable(request))
            {
                Approval approvalEntity = _mapper.Map<Approval>(approval);
                request.InstructorApproval = approvalEntity;

                if (!approvalEntity.IsApproved)
                {
                    request.IsArchived = true;
                    request.IsRejected = true;
                }
                else
                {
                    request.IsApproved = true;
                    request.IsArchived = true;

                    // log the equivalent course
                    await LogTheEquivalentCourse(requestDto);
                }

                await _notificationService.CreateNewApprovalNotification(request, FormType.EquivalanceRequest,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
                return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
            }
            return false;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }

            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            var coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    listToReturn.Add(request);
                }
            }

            return listToReturn;
        }

        public async Task<bool> CancelEquivalenceRequest(Guid requestId)
        {
            EquivalenceRequest request = _equivalenceRequestRepository.GetEquivalenceRequest(requestId).Result;
            if (CheckIfRequestIsOperable(request))
            {
                if (request == null)
                {
                    return false;
                }

                request.IsCanceled = true;
                request.IsArchived = true;

                return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
            }
            return false;
        }

        public async Task<bool> ArchiveEquivalenceRequest(Guid requestId)
        {
            EquivalenceRequest request = _equivalenceRequestRepository.GetEquivalenceRequest(requestId).Result;

            if (request == null)
            {
                return false;
            }

            if (CheckIfRequestIsOperable(request))
            {
                request.IsArchived = true;
                return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
            }
            return false;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequest> requests = await _equivalenceRequestRepository.GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequest request in requests)
            {
                if (request.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<EquivalenceRequestDto>(request));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequests()
        {
            IEnumerable<EquivalenceRequest> requests = await _equivalenceRequestRepository.GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequest request in requests)
            {
                if (!request.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<EquivalenceRequestDto>(request));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetNonArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalanceRequests = await GetNonArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<(byte[], string)> DownloadSyllabus(Guid id)
        {
            var request = await _equivalenceRequestRepository.GetEquivalenceRequest(id);

            if (request == null)
            {
                return (null, null);
            }
            return (request.Syllabus, request.FileName);
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

        private bool CheckIfRequestIsOperable(EquivalenceRequest request)
        {
            return !request.IsApproved && !request.IsRejected && !request.IsArchived && !request.IsCanceled;
        }

        private async Task<bool> LogTheEquivalentCourse(EquivalenceRequestDto equivalentRequest)
        {
            var form = new LoggedEquivalantCourseDto
            {
                ExemptedCourse = equivalentRequest.ExemptedCourse,
                HostCourseCode = equivalentRequest.HostCourseCode,
                HostCourseName = equivalentRequest.HostCourseName,
                HostCourseECTS = equivalentRequest.HostCourseECTS
            };

            form.ExemptedCourse.Id = Guid.NewGuid();

            return await _loggedCourseService.CreateLoggedEquivalantCourse(form);
        }
    }
}
