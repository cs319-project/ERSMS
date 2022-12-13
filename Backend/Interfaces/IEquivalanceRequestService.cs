using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface IEquivalanceRequestService
    {
        Task<bool> AddEquivalanceRequestToStudent(EquivalanceRequestDto equivalanceRequest);
        Task<IEnumerable<EquivalanceRequestDto>> GetEquivalanceRequests();
        Task<bool> DeleteEquivalanceRequest(Guid id);
        Task<EquivalanceRequestDto> GetEquivalanceRequest(Guid id);
        Task<bool> UpdateEquivalanceRequest(EquivalanceRequestDto equivalanceRequest);
        Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestsOfStudent(string studentID);
        // No real use
        Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestByCourseCode(string courseCode);
        Task<bool> ApproveRequest(Guid requestId, ApprovalDto approval);
        Task<bool> CancelEquivalanceRequest(Guid requestId);
        Task<bool> ArchiveEquivalanceRequest(Guid requestId);
        Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequests();
        Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequests();
        Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequestsByDepartmentForCoordinator(string userName);
        Task<ICollection<EquivalanceRequestDto>> GetArchivedEquivalanceRequestsByCourseCode(string courseCode);
        Task<ICollection<EquivalanceRequestDto>> GetNonArchivedEquivalanceRequestsByCourseCode(string courseCode);
    }
}
