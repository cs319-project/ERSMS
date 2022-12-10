using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IEquivalanceRequestRepository
    {
        Task<bool> AddEquivalanceRequestToStudent(string userName, EquivalanceRequest equivalanceRequest);
        Task<IEnumerable<EquivalanceRequest>> GetEquivalanceRequests();
        Task<bool> DeleteEquivalanceRequest(Guid id);
        Task<EquivalanceRequest> GetEquivalanceRequest(Guid id);
        Task<bool> UpdateEquivalanceRequest(EquivalanceRequest equivalanceRequest);

    }
}
