using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface ICTEFormService
    {
        Task<bool> AddCTEFormToStudent(CTEFormDto cTEForm);
        Task<IEnumerable<CTEFormDto>> GetCTEForms();
        Task<bool> DeleteCTEForm(Guid id);
        Task<CTEFormDto> GetCTEForm(Guid id);
        Task<ICollection<CTEFormDto>> GetCTEFormsOfStudent(string studentID);
        Task<bool> ApproveFormDean(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFormChair(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval);
    }
}
