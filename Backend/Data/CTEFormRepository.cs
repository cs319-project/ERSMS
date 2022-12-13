using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class CTEFormRepository : ICTEFormRepository
    {
        private readonly DataContext _context;

        // CONSTUCTOR
        public CTEFormRepository(DataContext context)
        {
            _context = context;
        }

        // METHODS
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

        public async Task<IEnumerable<CTEForm>> GetCTEForms()
        {
            var cTEForms = await _context.CTEForms.ToListAsync();
            return cTEForms;
        }

        public async Task<bool> DeleteCTEForm(Guid id)
        {
            var cTEForm = await _context.CTEForms.FindAsync(id);
            _context.CTEForms.Remove(cTEForm); // if cascading fails in db student lists won't updated
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CTEForm> GetCTEForm(Guid id)
        {
            var cTEForm = await _context.CTEForms.FindAsync(id);
            return cTEForm;
        }

        public async Task<bool> UpdateCTEForm(CTEForm cTEForm)
        {
            _context.CTEForms.Update(cTEForm);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
