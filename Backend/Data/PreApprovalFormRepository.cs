using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for Pre-Approval forms.</summary>
    public class PreApprovalFormRepository : IPreApprovalFormRepository
    {
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="PreApprovalFormRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public PreApprovalFormRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Deletes a pre-approval form.</summary>
        /// <param name="id">The ID of the pre-approval form to delete.</param>
        /// <returns>True if the pre-approval form was deleted; otherwise, false.</returns>
        public async Task<bool> DeletePreApprovalForm(Guid id)
        {
            var form = await _context.PreApprovalForms.FindAsync(id);
            _context.PreApprovalForms.Remove(form);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a pre-approval form.</summary>
        /// <param name="id">The ID of the pre-approval form.</param>
        /// <returns>The pre-approval form.</returns>
        public async Task<PreApprovalForm> GetPreApprovalForm(Guid id)
        {
            var form = await _context.PreApprovalForms.FindAsync(id);
            return form;
        }

        /// <summary>Gets the pre-approval forms.</summary>
        /// <returns>The pre-approval forms.</returns>
        public async Task<IEnumerable<PreApprovalForm>> GetPreApprovalForms()
        {
            var forms = await _context.PreApprovalForms.ToListAsync();
            return forms;
        }

        /// <summary>Submits a pre-approval form.</summary>
        /// <param name="preApprovalForm">The pre-approval form.</param>
        /// <returns>Whether the pre-approval form was submitted successfully.</returns>
        public async Task<bool> SubmitPreApprovalForm(PreApprovalForm preApprovalForm)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.IdentityUser.UserName == preApprovalForm.IDNumber);

            if (student.PreApprovalForms == null)
            {
                student.PreApprovalForms = new List<PreApprovalForm>();
            }

            student.PreApprovalForms.Add(preApprovalForm);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Updates the pre-approval form.</summary>
        /// <param name="preApprovalForm">The pre-approval form.</param>
        /// <returns>Whether the update was successful.</returns>
        public async Task<bool> UpdatePreApprovalForm(PreApprovalForm preApprovalForm)
        {
            _context.PreApprovalForms.Update(preApprovalForm);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Uploads a PDF to the database.</summary>
        /// <param name="formId">The ID of the form.</param>
        /// <param name="pdf">The PDF to upload.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>Whether the upload was successful.</returns>
        public async Task<bool> UploadPdf(Guid formId, byte[] pdf, string fileName)
        {
            var form = await _context.PreApprovalForms.FindAsync(formId);
            form.PDF = pdf;
            form.FileName = fileName;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
