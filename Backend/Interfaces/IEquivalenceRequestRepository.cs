using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IEquivalenceRequestRepository
    {
        Task<bool> AddEquivalenceRequestToStudent(string userName, EquivalenceRequest equivalenceRequest);
        Task<IEnumerable<EquivalenceRequest>> GetEquivalenceRequests();
        Task<bool> DeleteEquivalenceRequest(Guid id);
        Task<EquivalenceRequest> GetEquivalenceRequest(Guid id);
        Task<bool> UpdateEquivalenceRequest(EquivalenceRequest equivalenceRequest);

    }
}
