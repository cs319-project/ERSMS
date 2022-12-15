using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class LoggedCourseRepository : ILoggedCourseRepository
    {
        private readonly DataContext _context;

        // Constructor
        public LoggedCourseRepository(DataContext context)
        {
            _context = context;
        }

        // Methods
        public async Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse)
        {
            _context.LoggedEquivalantCourses.Add(loggedEquivalantCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse)
        {
            _context.LoggedTransferredCourses.Add(loggedTransferredCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLoggedEquivalantCourse(Guid id)
        {
            var course = await _context.LoggedEquivalantCourses.FindAsync(id);
            if (course == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteLoggedTransferredCourse(Guid id)
        {
            var course = await _context.LoggedTransferredCourses.FindAsync(id);
            if (course == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<LoggedEquivalentCourse> GetLoggedEquivalantCourse(Guid id)
        {
            return await _context.LoggedEquivalantCourses.FindAsync(id);
        }

        public async Task<IEnumerable<LoggedEquivalentCourse>> GetLoggedEquivalantCourses()
        {
            return await _context.LoggedEquivalantCourses.ToListAsync();
        }

        public async Task<LoggedTransferredCourse> GetLoggedTransferredCourse(Guid id)
        {
            return await _context.LoggedTransferredCourses.FindAsync(id);
        }

        public async Task<IEnumerable<LoggedTransferredCourse>> GetLoggedTransferredCourses()
        {
            return await _context.LoggedTransferredCourses.ToListAsync();
        }

        public async Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse)
        {
            _context.LoggedEquivalantCourses.Update(loggedEquivalantCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse)
        {
            _context.LoggedTransferredCourses.Update(loggedTransferredCourse);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
