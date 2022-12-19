using Backend.DTOs;

namespace Backend.Interfaces
{
    /// <summary>An interface for the pre-approval form service.</summary>
    public interface IPreApprovalFormService
    {
        Task<bool> SubmitPreApprovalForm(PreApprovalFormDto preApprovalForm);
        Task<IEnumerable<PreApprovalFormDto>> GetPreApprovalForms();
        Task<bool> DeletePreApprovalForm(Guid id);
        Task<PreApprovalFormDto> GetPreApprovalForm(Guid id);
        Task<bool> UpdatePreApprovalForm(PreApprovalFormDto preApprovalForm);
        Task<ICollection<PreApprovalFormDto>> GetPreApprovalFormsOfStudent(string studentID);
        Task<ICollection<PreApprovalFormDto>> GetPreApprovalFormsByDepartment(string userName);
        Task<bool> ApproveFormCoordinator(Guid formId, ApprovalDto approval);
        Task<bool> ApproveFormFacultyAdministrationBoard(Guid formId, ApprovalDto approval);
        Task<bool> CancelPreApprovalForm(Guid formId);
        Task<bool> ArchivePreApprovalForm(Guid formId);
        Task<ICollection<PreApprovalFormDto>> GetArchivedPreApprovalForms();
        Task<ICollection<PreApprovalFormDto>> GetNonArchivedPreApprovalForms();
        Task<ICollection<PreApprovalFormDto>> GetArchivedPreApprovalFormsByDepartment(string userName);
        Task<ICollection<PreApprovalFormDto>> GetNonArchivedPreApprovalFormsByDepartment(string userName);
        Task<(byte[], string)> DownloadPdf(Guid formId);
        Task<bool> UploadPdf(Guid formId, IFormFile fileName);
        Task<bool> DeletePdf(Guid formId);
    }
}
