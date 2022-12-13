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
    public class EquivalanceRequestService : IEquivalanceRequestService
    {
        private readonly IEquivalanceRequestRepository _equivalanceRequestRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public EquivalanceRequestService(IEquivalanceRequestRepository equivalanceRequestRepository,
                                            IUserRepository userRepository, IUserService userService, IMapper mapper)
        {
            _equivalanceRequestRepository = equivalanceRequestRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        // Method
        public async Task<bool> AddEquivalanceRequestToStudent(EquivalanceRequestDto equivalanceRequest)
        {
            // change if necessary for files
            return await _equivalanceRequestRepository.AddEquivalanceRequestToStudent(equivalanceRequest.StudentId, _mapper.Map<EquivalanceRequest>(equivalanceRequest));
        }

        public async Task<IEnumerable<EquivalanceRequestDto>> GetEquivalanceRequests()
        {
            return _mapper.Map<IEnumerable<EquivalanceRequestDto>>(await _equivalanceRequestRepository.GetEquivalanceRequests());
        }

        public async Task<bool> DeleteEquivalanceRequest(Guid id)
        {
            return await _equivalanceRequestRepository.DeleteEquivalanceRequest(id);
        }

        public async Task<EquivalanceRequestDto> GetEquivalanceRequest(Guid id)
        {
            return _mapper.Map<EquivalanceRequestDto>(await _equivalanceRequestRepository.GetEquivalanceRequest(id));
        }

        public async Task<bool> UpdateEquivalanceRequest(EquivalanceRequestDto equivalanceRequest)
        {
            return await _equivalanceRequestRepository.UpdateEquivalanceRequest(_mapper.Map<EquivalanceRequest>(equivalanceRequest));
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestsOfStudent(string studentID)
        {
            Student student = await _userRepository.GetStudentByUserName(studentID);
            ICollection<EquivalanceRequestDto> equivalanceRequests = _mapper.Map<ICollection<EquivalanceRequestDto>>(student.EquivalanceRequestForms);
            return equivalanceRequests;
        }

        public async Task<bool> ApproveRequest(Guid requestId, ApprovalDto approval)
        {
            EquivalanceRequest request = await _equivalanceRequestRepository.GetEquivalanceRequest(requestId);
            if (request == null)
            {
                return false;
            }
            Approval approvalEntity = _mapper.Map<Approval>(approval);
            request.InstructorApproval = approvalEntity;

            return await _equivalanceRequestRepository.UpdateEquivalanceRequest(request);
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestByCourseCode(string courseCode)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }

            return listToReturn;
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestsByDepartmentForCoordinator(string userName)
        {
            var coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
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

        public async Task<bool> CancelEquivalanceRequest(Guid requestId)
        {
            EquivalanceRequest request = _equivalanceRequestRepository.GetEquivalanceRequest(requestId).Result;
            if (CheckIfFormIsOperable(request))
            {
                if (request == null)
                {
                    return false;
                }

                request.IsCanceled = true;
                request.IsArchived = true;

                return await _equivalanceRequestRepository.UpdateEquivalanceRequest(request);
            }
            return false;
        }

        public async Task<bool> ArchiveEquivalanceRequest(Guid requestId)
        {
            EquivalanceRequest request = _equivalanceRequestRepository.GetEquivalanceRequest(requestId).Result;

            if (request == null)
            {
                return false;
            }

            if (CheckIfFormIsOperable(request))
            {
                request.IsArchived = true;
                return await _equivalanceRequestRepository.UpdateEquivalanceRequest(request);
            }
            return false;
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequests()
        {
            IEnumerable<EquivalanceRequest> requests = await _equivalanceRequestRepository.GetEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequest request in requests)
            {
                if (request.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<EquivalanceRequestDto>(request));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequests()
        {
            IEnumerable<EquivalanceRequest> requests = await _equivalanceRequestRepository.GetEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequest request in requests)
            {
                if (!request.IsArchived)
                {
                    listToReturn.Add(_mapper.Map<EquivalanceRequestDto>(request));
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetArchivedEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
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

        public async Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequestsByDepartmentForCoordinator(string userName)
        {
            ExchangeCoordinator coordinator = await _userService.GetExchangeCoordinator(userName);
            Department department = coordinator.Department.DepartmentName;
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetNonArchivedEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
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

        public async Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetArchivedEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        public async Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequestsByCourseCode(string courseCode)
        {
            IEnumerable<EquivalanceRequestDto> equivalanceRequests = await GetNonArchivedEquivalanceRequests();
            ICollection<EquivalanceRequestDto> listToReturn = new List<EquivalanceRequestDto>();

            foreach (EquivalanceRequestDto request in equivalanceRequests)
            {
                if (request.ExemptedCourse.CourseCode == courseCode)
                {
                    listToReturn.Add(request);
                }
            }
            return listToReturn;
        }

        private bool CheckIfFormIsOperable(EquivalanceRequest request)
        {
            return !request.IsArchived && !request.IsCanceled && !request.IsApproved && !request.IsRejected;
        }
    }
}
