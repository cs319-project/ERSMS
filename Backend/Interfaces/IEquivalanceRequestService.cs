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
        //Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestsByDepartment(string userName);
        Task<ICollection<EquivalanceRequestDto>> GetEquivalanceRequestByCourseCode(string courseCode);
        Task<bool> ApproveRequest(Guid requestId, ApprovalDto approval);
    }
}
