using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the equivalence request repository.</summary>
    public interface IEquivalenceRequestRepository
    {
        Task<bool> AddEquivalenceRequestToStudent(string userName, EquivalenceRequest equivalenceRequest);
        Task<IEnumerable<EquivalenceRequest>> GetEquivalenceRequests();
        Task<bool> DeleteEquivalenceRequest(Guid id);
        Task<EquivalenceRequest> GetEquivalenceRequest(Guid id);
        Task<bool> UpdateEquivalenceRequest(EquivalenceRequest equivalenceRequest);

    }
}
