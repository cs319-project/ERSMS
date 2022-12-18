using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    /// <summary>An interface for the CTE (Course Transfer and Exemption) form service.</summary>
    public interface ICTEFormService
    {
        Task<bool> AddCTEFormToStudent(CTEFormDto cTEForm);
        Task<ICollection<CTEFormDto>> GetNonArchivedCTEFormsByFacultyForDean(string userName);
        Task<ICollection<CTEFormDto>> GetArchivedCTEFormsByFacultyForDean(string userName);
        Task<(byte[], string)> DownloadPdf(Guid formId);
        Task<IEnumerable<CTEFormDto>> GetCTEForms();
        Task<bool> DeleteCTEForm(Guid id);
        Task<CTEFormDto> GetCTEForm(Guid id);
        Task<bool> UpdateCTEForm(CTEFormDto cTEForm);
        Task<ICollection<CTEFormDto>> GetCTEFormsOfStudent(string studentID);
        Task<ICollection<CTEFormDto>> GetCTEFormsByDepartment(string userName);
        Task<bool> ApproveFormDean(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFormChair(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFacultyOfAdministrationBoard(Guid formId, ApprovalDto approval);
        Task<bool> CancelCTEForm(Guid formId);
        Task<bool> ArchiveCTEForm(Guid formId);
        Task<ICollection<CTEFormDto>> GetArchivedCTEForms();
        Task<ICollection<CTEFormDto>> GetNonArchivedCTEForms();
        Task<ICollection<CTEFormDto>> GetArchivedCTEFormsByDepartment(string userName);
        Task<ICollection<CTEFormDto>> GetNonArchivedCTEFormsByDepartment(string userName);
        Task<bool> UploadPdf(Guid formId, IFormFile fileName);
    }
}
