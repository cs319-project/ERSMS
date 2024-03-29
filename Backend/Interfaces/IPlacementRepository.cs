using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the placement repository.</summary>
    public interface IPlacementRepository
    {
        Task<bool> UploadPlacementTable(PlacementTable placementTable);
        Task<PlacementTable> GetPlacementTable(Guid id);
        Task<bool> DeletePlacementTable(Guid id);
        Task<IEnumerable<PlacementTable>> GetAllPlacementTables();
        Task<bool> PlaceStudent(PlacedStudent placedStudent);
        Task<PlacedStudent> GetPlacedStudent(String userName);

    }
}
