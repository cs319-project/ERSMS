using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.DTOs;

namespace Backend.Interfaces
{
    /// <summary>An interface for the placement service.</summary>
    public interface IPlacementService
    {
        Task<PlacementTableDto> UploadPlacementTable(String facultyName, String departmentName, IFormFile placementTable);
        Task<(byte[], string)> DownloadPlacementTable(Guid id);
        Task<bool> DeletePlacementTable(Guid id);
        Task<IEnumerable<PlacementTableDto>> GetAllPlacementTables();
        Task<IEnumerable<PlacementTableDto>> GetPlacementTablesByDepartment(String facultyName, String departmentName);
        Task<PlacementTableDto> GetPlacementTable(Guid id);
        Task<IEnumerable<PlacedStudentDto>> PlaceStudents(Guid id);
    }
}
