using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface IPreApprovalFormService
    {
        Task<bool> SubmitPreApprovalForm(PreApprovalFormDto preApprovalForm);
        Task<IEnumerable<PreApprovalFormDto>> GetPreApprovalForms();
        Task<bool> DeletePreApprovalForm(Guid id);
        Task<PreApprovalFormDto> GetPreApprovalForm(Guid id);
        Task<bool> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm);
        Task<ICollection<PreApprovalFormDto>> GetPreApprovalFormsOfStudent(string studentID);
        Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval);
    }
}