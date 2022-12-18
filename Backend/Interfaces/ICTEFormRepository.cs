using Backend.Entities;

namespace Backend.Interfaces
{
    /// <summary>An interface for the CTE (Course Transfer and Exemption) form repository.</summary>
    public interface ICTEFormRepository
    {
        Task<bool> AddCTEFormToStudent(string userName, CTEForm cTEForm);
        Task<IEnumerable<CTEForm>> GetCTEForms();
        Task<bool> DeleteCTEForm(Guid id);
        Task<CTEForm> GetCTEForm(Guid id);
        Task<bool> UpdateCTEForm(CTEForm cTEForm);
        Task<bool> UploadPdf(Guid formId, byte[] pdf, string fileName);
    }
}
