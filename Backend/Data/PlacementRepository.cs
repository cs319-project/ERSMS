using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>Provides access to the placement repository.</summary>
    public class PlacementRepository : IPlacementRepository
    {

        private readonly DataContext _context;


        /// <summary>Initializes a new instance of the <see cref="PlacementRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public PlacementRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Uploads a placement table to the database.</summary>
        /// <param name="placementTable">The placement table to upload.</param>
        /// <returns>Whether the upload was successful.</returns>
        public async Task<bool> UploadPlacementTable(PlacementTable placementTable)
        {
            // Placement table with same department and faculty name already exists
            // replace the existing placement table with the new one
            // var existingPlacementTable = _context.PlacementTables.FirstOrDefault(pt => pt.Department.DepartmentName == placementTable.Department.DepartmentName && pt.Department.FacultyName == placementTable.Department.FacultyName);
            // if (existingPlacementTable != null)
            // {
            //     _context.PlacementTables.Remove(existingPlacementTable);
            // }
            _context.PlacementTables.Add(placementTable);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets the placement table with the specified ID.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placement table with the specified ID.</returns>
        public async Task<PlacementTable> GetPlacementTable(Guid id)
        {
            return await _context.PlacementTables.SingleOrDefaultAsync(pt => pt.Id == new Guid(id.ToString()));
        }

        /// <summary>Deletes a placement table.</summary>
        /// <param name="id">The ID of the placement table to delete.</param>
        /// <returns>True if the placement table was deleted; otherwise, false.</returns>
        public async Task<bool> DeletePlacementTable(Guid id)
        {
            var placementTable = await _context.PlacementTables.SingleOrDefaultAsync(pt => pt.Id == new Guid(id.ToString()));
            if (placementTable == null)
            {
                return false;
            }
            _context.PlacementTables.Remove(placementTable);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets all placement tables.</summary>
        /// <returns>All placement tables.</returns>
        public async Task<IEnumerable<PlacementTable>> GetAllPlacementTables()
        {
            return await _context.PlacementTables.ToListAsync();
        }

        /// <summary>Places a student in the database.</summary>
        /// <param name="placedStudent">The student to place.</param>
        /// <returns>Whether the student was placed successfully.</returns>
        public async Task<bool> PlaceStudent(PlacedStudent placedStudent)
        {
            var placedStudents = _context.PlacedStudents;

            // Check if the student is already placed
            var existingPlacedStudent = placedStudents.FirstOrDefaultAsync(ps => ps.UserName == placedStudent.UserName).Result;

            if (existingPlacedStudent != null)
            {
                placedStudent.Id = existingPlacedStudent.Id;
                _context.Entry(existingPlacedStudent).CurrentValues.SetValues(placedStudent);
            }

            else
            {
                placedStudents.Add(placedStudent);
            }

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a placed student.</summary>
        /// <param name="studentId">The student id.</param>
        /// <returns>The placed student.</returns>
        public async Task<PlacedStudent> GetPlacedStudent(String studentId)
        {
            return await _context.PlacedStudents.SingleAsync(ps => ps.UserName == studentId);
        }
    }
}
