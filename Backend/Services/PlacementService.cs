using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;

namespace Backend.Services
{
    /// <summary>A service for student placement operations.</summary>
    public class PlacementService : IPlacementService
    {
        private readonly IPlacementRepository _placementRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="PlacementService"/> class.</summary>
        /// <param name="placementRepository">The repository for storing and retrieving placements.</param>
        /// <param name="mapper">The mapper for mapping domain objects to DTOs and vice versa.</param>
        public PlacementService(IPlacementRepository placementRepository, IMapper mapper)
        {
            _placementRepository = placementRepository;
            _mapper = mapper;
        }

        /// <summary>Converts a file to a byte array.</summary>
        /// <param name="file">The file.</param>
        /// <returns>Byte array of the file</returns>
        private async Task<byte[]> SaveFile(IFormFile file)
        {
            // convert file to byte array
            byte[] fileBytes;

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileBytes = ms.ToArray();
            }

            return fileBytes;
        }

        /// <summary>Uploads a placement table by the given faculty and department.</summary>
        /// <param name="facultyName">The faculty name which the placement table belongs to.</param>
        /// <param name="departmentName">The department name which the placement table belongs to.</param>
        /// <param name="placementTable">The placement table's (exchange score table) Excel file.</param>
        /// <returns>The placement table DTO.</returns>
        public async Task<PlacementTableDto> UploadPlacementTable(String facultyName, String departmentName, IFormFile placementTable)
        {
            if (Path.GetExtension(placementTable.FileName) != ".xlsx" && Path.GetExtension(placementTable.FileName) != ".xls")
            {
                return null;
            }

            else
            {

                var department = new DepartmentInfo()
                {
                    FacultyName = EnumStringify.FacultyEnumarator(facultyName),
                    DepartmentName = EnumStringify.DepartmentEnumarator(departmentName)
                };

                var placementTableObject = new PlacementTable()
                {
                    Id = Guid.NewGuid(),
                    Department = department,
                    ExcelFile = await SaveFile(placementTable),
                    FileName = placementTable.FileName,
                    UploadTime = DateTime.Now
                };

                var success = await _placementRepository.UploadPlacementTable(placementTableObject);

                if (success)
                {
                    return _mapper.Map<PlacementTableDto>(placementTableObject);
                }
                else return null;
            }
        }

        /// <summary>Downloads the placement table.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placement table as an Excel file.</returns>
        public async Task<(byte[], string)> DownloadPlacementTable(Guid id)
        {
            var placementTable = await _placementRepository.GetPlacementTable(id);

            if (placementTable != null)
            {
                return (placementTable.ExcelFile, placementTable.FileName);
            }
            else return (null, null);
        }

        /// <summary>Deletes the placement table with the specified ID.</summary>
        /// <param name="id">The ID of the placement table to delete.</param>
        /// <returns>True if the placement table was deleted successfully, false otherwise.</returns>
        public async Task<bool> DeletePlacementTable(Guid id)
        {
            return await _placementRepository.DeletePlacementTable(id);
        }

        /// <summary>Gets all placement tables.</summary>
        /// <returns>A list of all placement tables.</returns>
        public async Task<IEnumerable<PlacementTableDto>> GetAllPlacementTables()
        {
            var placementTables = await _placementRepository.GetAllPlacementTables();
            return _mapper.Map<IEnumerable<PlacementTableDto>>(placementTables);
        }

        /// <summary>Retrieves all placement tables for a given department.</summary>
        /// <param name="facultyName">The faculty name.</param>
        /// <param name="departmentName">The department name.</param>
        /// <returns>The placement tables for the given department.</returns>
        public async Task<IEnumerable<PlacementTableDto>> GetPlacementTablesByDepartment(String facultyName, String departmentName)
        {
            var placementTables = await _placementRepository.GetAllPlacementTables();
            var department = new DepartmentInfo()
            {
                FacultyName = EnumStringify.FacultyEnumarator(facultyName),
                DepartmentName = EnumStringify.DepartmentEnumarator(departmentName)
            };

            var filteredPlacementTables = placementTables.Where(x => x.Department.Equals(department));

            return _mapper.Map<IEnumerable<PlacementTableDto>>(filteredPlacementTables);
        }

        /// <summary>Gets the placement table.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placement table.</returns>
        public async Task<PlacementTableDto> GetPlacementTable(Guid id)
        {
            var placementTable = await _placementRepository.GetPlacementTable(id);
            return _mapper.Map<PlacementTableDto>(placementTable);
        }

        /// <summary>Places students in the specified placement table.</summary>
        /// <param name="id">The ID of the placement table.</param>
        /// <returns>The placed students.</returns>
        public async Task<IEnumerable<PlacedStudentDto>> PlaceStudents(Guid id)
        {
            var placementTable = await _placementRepository.GetPlacementTable(id);
            var excelFile = placementTable.ExcelFile;

            var placedStudents = AutoPlacer.PlaceStudents(excelFile);

            // Then import into PlacedStudents table
            foreach (var placedStudent in placedStudents)
            {
                placedStudent.Id = Guid.NewGuid();
                await _placementRepository.PlaceStudent(placedStudent);
            }

            return _mapper.Map<IEnumerable<PlacedStudentDto>>(placedStudents);
        }
    }
}
