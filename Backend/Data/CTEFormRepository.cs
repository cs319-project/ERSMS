using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for CTE (Course Transfer and Exemption) forms.</summary>
    public class CTEFormRepository : ICTEFormRepository
    {
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="CTEFormRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public CTEFormRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Adds a CTE form to a student.</summary>
        /// <param name="id">The ID of the student.</param>
        /// <param name="cTEForm">The CTE form to add.</param>
        /// <returns>True if the CTE form was added successfully, false otherwise.</returns>
        public async Task<bool> AddCTEFormToStudent(string id, CTEForm cTEForm)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.IdentityUser.UserName == id);

            if (student.CTEForms == null)
            {
                student.CTEForms = new List<CTEForm>();
            }

            student.CTEForms.Add(cTEForm);
            //_context.CTEForms.Add(cTEForm); // CAN CREATE DUPLICATION IN DATABASE
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets the CTE forms.</summary>
        /// <returns>The CTE forms.</returns>
        public async Task<IEnumerable<CTEForm>> GetCTEForms()
        {
            var cTEForms = await _context.CTEForms.ToListAsync();
            return cTEForms;
        }

        /// <summary>Deletes a CTEForm.</summary>
        /// <param name="id">The ID of the CTEForm to delete.</param>
        /// <returns>True if the CTEForm was deleted, false otherwise.</returns>
        public async Task<bool> DeleteCTEForm(Guid id)
        {
            var cTEForm = await _context.CTEForms.FindAsync(id);
            _context.CTEForms.Remove(cTEForm); // if cascading fails in db student lists won't updated
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Retrieves a CTEForm from the database.</summary>
        /// <param name="id">The ID of the CTEForm to retrieve.</param>
        /// <returns>The CTEForm with the specified ID.</returns>
        public async Task<CTEForm> GetCTEForm(Guid id)
        {
            var cTEForm = await _context.CTEForms.FindAsync(id);
            return cTEForm;
        }

        /// <summary>Updates the specified CTEForm in the database.</summary>
        /// <param name="cTEForm">The CTEForm to update.</param>
        /// <returns>True if the CTEForm was updated, false otherwise.</returns>
        public async Task<bool> UpdateCTEForm(CTEForm cTEForm)
        {
            _context.CTEForms.Update(cTEForm);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Uploads a PDF to the database.</summary>
        /// <param name="formId">The ID of the form.</param>
        /// <param name="pdf">The PDF to upload.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>Whether the upload was successful.</returns>
        public async Task<bool> UploadPdf(Guid formId, byte[] pdf, string fileName)
        {
            var form = await _context.CTEForms.FindAsync(formId);
            form.PDF = pdf;
            form.FileName = fileName;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
