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
    public class PlacementService : IPlacementService
    {
        private readonly IPlacementRepository _placementRepository;
        private readonly IMapper _mapper;

        public PlacementService(IPlacementRepository placementRepository, IMapper mapper)
        {
            _placementRepository = placementRepository;
            _mapper = mapper;
        }

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

        public async Task<(byte[], string)> DownloadPlacementTable(Guid id)
        {
            var placementTable = await _placementRepository.GetPlacementTable(id);

            if (placementTable != null)
            {
                return (placementTable.ExcelFile, placementTable.FileName);
            }
            else return (null, null);
        }

        public async Task<bool> DeletePlacementTable(Guid id)
        {
            return await _placementRepository.DeletePlacementTable(id);
        }

        public async Task<IEnumerable<PlacementTableDto>> GetAllPlacementTables()
        {
            var placementTables = await _placementRepository.GetAllPlacementTables();
            return _mapper.Map<IEnumerable<PlacementTableDto>>(placementTables);
        }

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

        public async Task<PlacementTableDto> GetPlacementTable(Guid id)
        {
            var placementTable = await _placementRepository.GetPlacementTable(id);
            return _mapper.Map<PlacementTableDto>(placementTable);
        }

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
