using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class PlacementRepository : IPlacementRepository
    {

        private readonly DataContext _context;


        public PlacementRepository(DataContext context)
        {
            _context = context;
        }

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

        public async Task<PlacementTable> GetPlacementTable(Guid id)
        {
            return await _context.PlacementTables.SingleOrDefaultAsync(pt => pt.Id == new Guid(id.ToString()));
        }

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

        public async Task<IEnumerable<PlacementTable>> GetAllPlacementTables()
        {
            return await _context.PlacementTables.ToListAsync();
        }

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
    }
}
