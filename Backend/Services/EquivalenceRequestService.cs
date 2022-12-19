using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Entities.Exceptions;
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
            var logs = (await _loggedCourseService.GetLoggedEquivalantCourses()).Select(x => (x.HostCourseCode == equivalenceRequest.HostCourseCode)
                        && (x.HostSchool == equivalenceRequest.HostUniversityName));
            foreach (var log in logs)
            {
                if (log) { throw new AlreadyLoggedException("Course with code " + equivalenceRequest.HostCourseCode + " already logged"); }
            }
            EquivalenceRequest request = _mapper.Map<EquivalenceRequest>(equivalenceRequest);
            request.Syllabus = await SaveFile(file);
            var flag = await _equivalenceRequestRepository.AddEquivalenceRequestToStudent(equivalenceRequest.StudentId, request);

            if (flag)
            {
                await _notificationService.CreateNewFormNotification(request, FormType.EquivalenceRequest);
            }

            return flag;
        }

        public async Task<IEnumerable<EquivalenceRequestDto>> GetEquivalenceRequests()
        {
            var result = _mapper.Map<IEnumerable<EquivalenceRequestDto>>(await _equivalenceRequestRepository.GetEquivalenceRequests());

            foreach (var request in result)
            {
                var student = await _userService.GetUser(request.StudentId) as StudentDto;
                request.FirstName = student.FirstName;
                request.LastName = student.LastName;
                request.HostUniversityName = student.ExchangeSchool;
            }

            return result;
        }

        public async Task<bool> DeleteEquivalenceRequest(Guid id)
        {
            return await _equivalenceRequestRepository.DeleteEquivalenceRequest(id);
        }

        public async Task<EquivalenceRequestDto> GetEquivalenceRequest(Guid id)
        {
            var request = _mapper.Map<EquivalenceRequestDto>(await _equivalenceRequestRepository.GetEquivalenceRequest(id));

            var student = await _userService.GetUser(request.StudentId) as StudentDto;
            request.FirstName = student.FirstName;
            request.LastName = student.LastName;
            request.HostUniversityName = student.ExchangeSchool;

            return request;
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
            ICollection<EquivalenceRequestDto> equivalenceRequests = _mapper.Map<ICollection<EquivalenceRequestDto>>(student.EquivalenceRequestForms);

            foreach (var request in equivalenceRequests)
            {
                request.FirstName = student.FirstName;
                request.LastName = student.LastName;
                request.HostUniversityName = student.ExchangeSchool;
            }

            return equivalenceRequests;
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

                await _notificationService.CreateNewApprovalNotification(request, FormType.EquivalenceRequest,
                                                                            approvalEntity.IsApproved, approvalEntity.Name);
                return await _equivalenceRequestRepository.UpdateEquivalenceRequest(request);
            }
            return false;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    var student = await _userService.GetStudent(request.StudentId);
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(request);
                }
            }

            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            var coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;
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
                    var requestTemp = _mapper.Map<EquivalenceRequestDto>(request);

                    var student = await _userService.GetStudent(request.StudentId);
                    requestTemp.FirstName = student.FirstName;
                    requestTemp.LastName = student.LastName;
                    requestTemp.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(requestTemp);
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
                    var requestTemp = _mapper.Map<EquivalenceRequestDto>(request);

                    var student = await _userService.GetStudent(request.StudentId);
                    requestTemp.FirstName = student.FirstName;
                    requestTemp.LastName = student.LastName;
                    requestTemp.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(requestTemp);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetNonArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                var student = await _userService.GetStudent(request.StudentId);
                if (student == null)
                {
                    continue;
                }
                if (student.Major.DepartmentName == department)
                {
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(request);
                }
            }

            foreach (var request in listToReturn)
            {
                var student = await _userService.GetUser(request.StudentId) as StudentDto;
                request.FirstName = student.FirstName;
                request.LastName = student.LastName;
                request.HostUniversityName = student.ExchangeSchool;
            }

            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    var student = await _userService.GetStudent(request.StudentId);
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalenceRequestDto> equivalenceRequests = await GetNonArchivedEquivalenceRequests();
            ICollection<EquivalenceRequestDto> listToReturn = new List<EquivalenceRequestDto>();

            foreach (EquivalenceRequestDto request in equivalenceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    var student = await _userService.GetStudent(request.StudentId);
                    request.FirstName = student.FirstName;
                    request.LastName = student.LastName;
                    request.HostUniversityName = student.ExchangeSchool;

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
            var form = new LoggedEquivalentCourseDto
            {
                ExemptedCourse = equivalentRequest.ExemptedCourse,
                HostCourseCode = equivalentRequest.HostCourseCode,
                HostCourseName = equivalentRequest.HostCourseName,
                HostCourseECTS = equivalentRequest.HostCourseECTS,
                HostSchool = equivalentRequest.HostUniversityName
            };

            form.ExemptedCourse.Id = Guid.NewGuid();

            return await _loggedCourseService.CreateLoggedEquivalantCourse(form);
        }
    }
}
