using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class EquivalanceRequestRepository : IEquivalanceRequestRepository
    {

        private readonly DataContext _context;

        // Constructor
        public EquivalanceRequestRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> AddEquivalanceRequestToStudent(string userName, EquivalanceRequest equivalanceRequest)
        {
            var student = _context.Students.FirstOrDefault(x => x.IdentityUser.UserName == userName);

            if (student.EquivalanceRequestForms == null) // if student has no equivalance requests
            {
                student.EquivalanceRequestForms = new List<EquivalanceRequest>();
            }

            student.EquivalanceRequestForms.Add(equivalanceRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEquivalanceRequest(Guid id)
        {
            var request = await _context.EquivalanceRequests.FindAsync(id);
            _context.EquivalanceRequests.Remove(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<EquivalanceRequest> GetEquivalanceRequest(Guid id)
        {
            return await _context.EquivalanceRequests.FindAsync(id);
        }

        public async Task<IEnumerable<EquivalanceRequest>> GetEquivalanceRequests()
        {
            return await _context.EquivalanceRequests.ToListAsync();
        }

        public async Task<bool> UpdateEquivalanceRequest(EquivalanceRequest equivalanceRequest)
        {
            var request = _context.EquivalanceRequests.Update(equivalanceRequest);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
