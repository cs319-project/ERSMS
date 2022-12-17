using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class PreApprovalFormRepository : IPreApprovalFormRepository
    {
        private readonly DataContext _context;

        // Constructor
        public PreApprovalFormRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> DeletePreApprovalForm(Guid id)
        {
            var form = await _context.PreApprovalForms.FindAsync(id);
            _context.PreApprovalForms.Remove(form);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PreApprovalForm> GetPreApprovalForm(Guid id)
        {
            var form = await _context.PreApprovalForms.FindAsync(id);
            return form;
        }

        public async Task<IEnumerable<PreApprovalForm>> GetPreApprovalForms()
        {
            var forms = await _context.PreApprovalForms.ToListAsync();
            return forms;
        }

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

        public async Task<bool> UpdatePreApprovalForm(PreApprovalForm preApprovalForm)
        {
            _context.PreApprovalForms.Update(preApprovalForm);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UploadPdf(Guid formId, byte[] pdf, string fileName)
        {
            var form = await _context.PreApprovalForms.FindAsync(formId);
            form.PDF = pdf;
            form.FileName = fileName;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
