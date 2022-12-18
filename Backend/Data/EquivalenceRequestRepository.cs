using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class EquivalenceRequestRepository : IEquivalenceRequestRepository
    {

        private readonly DataContext _context;

        // Constructor
        public EquivalenceRequestRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> AddEquivalenceRequestToStudent(string userName, EquivalenceRequest equivalenceRequest)
        {
            var student = _context.Students.FirstOrDefault(x => x.IdentityUser.UserName == userName);

            if (student.EquivalenceRequestForms == null) // if student has no equivalence requests
            {
                student.EquivalenceRequestForms = new List<EquivalenceRequest>();
            }

            student.EquivalenceRequestForms.Add(equivalenceRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEquivalenceRequest(Guid id)
        {
            var request = await _context.EquivalenceRequests.FindAsync(id);
            _context.EquivalenceRequests.Remove(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<EquivalenceRequest> GetEquivalenceRequest(Guid id)
        {
            return await _context.EquivalenceRequests.FindAsync(id);
        }

        public async Task<IEnumerable<EquivalenceRequest>> GetEquivalenceRequests()
        {
            return await _context.EquivalenceRequests.ToListAsync();
        }

        public async Task<bool> UpdateEquivalenceRequest(EquivalenceRequest equivalenceRequest)
        {
            var request = _context.EquivalenceRequests.Update(equivalenceRequest);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
