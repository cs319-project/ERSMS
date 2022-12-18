using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for equivalence requests.</summary>
    public class EquivalenceRequestRepository : IEquivalenceRequestRepository
    {

        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="EquivalenceRequestRepository"/> class.</summary>
        /// <param name="context">The data context.</param>
        public EquivalenceRequestRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Adds an equivalence request to a student.</summary>
        /// <param name="userName">The user name of the student.</param>
        /// <param name="equivalenceRequest">The equivalence request to add.</param>
        /// <returns>True if the request was added successfully, false otherwise.</returns>
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

        /// <summary>Deletes an equivalence request.</summary>
        /// <param name="id">The ID of the equivalence request to delete.</param>
        /// <returns>True if the request was deleted, false otherwise.</returns>
        public async Task<bool> DeleteEquivalenceRequest(Guid id)
        {
            var request = await _context.EquivalenceRequests.FindAsync(id);
            _context.EquivalenceRequests.Remove(request);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets an equivalence request.</summary>
        /// <param name="id">The ID of the equivalence request.</param>
        /// <returns>The equivalence request.</returns>
        public async Task<EquivalenceRequest> GetEquivalenceRequest(Guid id)
        {
            return await _context.EquivalenceRequests.FindAsync(id);
        }

        /// <summary>Gets the equivalence requests.</summary>
        /// <returns>The equivalence requests.</returns>
        public async Task<IEnumerable<EquivalenceRequest>> GetEquivalenceRequests()
        {
            return await _context.EquivalenceRequests.ToListAsync();
        }

        /// <summary>Updates an equivalence request.</summary>
        /// <param name="equivalenceRequest">The equivalence request to update.</param>
        /// <returns>Whether the update was successful.</returns>
        public async Task<bool> UpdateEquivalenceRequest(EquivalenceRequest equivalenceRequest)
        {
            var request = _context.EquivalenceRequests.Update(equivalenceRequest);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
