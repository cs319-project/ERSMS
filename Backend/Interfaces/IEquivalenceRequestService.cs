using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    /// <summary>An interface for the equivalence request service.</summary>
    public interface IEquivalenceRequestService
    {
        Task<(byte[], string)> DownloadSyllabus(Guid id);
        Task<bool> AddEquivalenceRequestToStudent(EquivalenceRequestDto equivalenceRequest, IFormFile syllabus);
        Task<IEnumerable<EquivalenceRequestDto>> GetEquivalenceRequests();
        Task<bool> DeleteEquivalenceRequest(Guid id);
        Task<EquivalenceRequestDto> GetEquivalenceRequest(Guid id);
        Task<bool> UpdateEquivalenceRequest(EquivalenceRequestDto equivalenceRequest);
        Task<bool> UpdateEquivalenceRequestSyllabus(Guid id, IFormFile syllabus);
        Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestsOfStudent(string studentID);
        // No real use
        Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalenceRequestDto>> GetEquivalenceRequestByCourseCode(string courseCode);
        Task<bool> ApproveRequest(Guid requestId, ApprovalDto approval);
        Task<bool> CancelEquivalenceRequest(Guid requestId);
        Task<bool> ArchiveEquivalenceRequest(Guid requestId);
        Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequests();
        Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequests();
        Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalenceRequestDto>> GetArchivedEquivalenceRequestsByCourseCode(string courseCode);
        Task<ICollection<EquivalenceRequestDto>> GetNonArchivedEquivalenceRequestsByCourseCode(string courseCode);
    }
}
