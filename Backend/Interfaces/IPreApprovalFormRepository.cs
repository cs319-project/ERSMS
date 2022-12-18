using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the pre-approval form repository.</summary>
    public interface IPreApprovalFormRepository
    {
        Task<bool> SubmitPreApprovalForm(PreApprovalForm preApprovalForm);
        Task<IEnumerable<PreApprovalForm>> GetPreApprovalForms();
        Task<bool> DeletePreApprovalForm(Guid id);
        Task<PreApprovalForm> GetPreApprovalForm(Guid id);
        Task<bool> UpdatePreApprovalForm(PreApprovalForm preApprovalForm);
        Task<bool> UploadPdf(Guid formId, byte[] pdf, string fileName);
    }
}
