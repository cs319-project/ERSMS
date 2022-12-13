using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface IPreApprovalFormRepository
    {
        Task<bool> SubmitPreApprovalForm(PreApprovalForm preApprovalForm);
        Task<IEnumerable<PreApprovalForm>> GetPreApprovalForms();
        Task<bool> DeletePreApprovalForm(Guid id);
        Task<PreApprovalForm> GetPreApprovalForm(Guid id);
        Task<bool> UpdatePreApprovalForm(PreApprovalForm preApprovalForm);
    }
}
