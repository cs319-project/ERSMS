using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    /// <summary>A repository for the logged courses.</summary>
    public class LoggedCourseRepository : ILoggedCourseRepository
    {
        private readonly DataContext _context;

        /// <summary>Initializes a new instance of the <see cref="LoggedCourseRepository"/> class.</summary>
        /// <param name="context">The context to use.</param>
        public LoggedCourseRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>Creates a logged equivalent course.</summary>
        /// <param name="loggedEquivalantCourse">The logged equivalent course to create.</param>
        /// <returns>Whether the logged equivalent course was created successfully.</returns>
        public async Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse)
        {
            _context.LoggedEquivalentCourses.Add(loggedEquivalantCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Creates a new logged transferred course.</summary>
        /// <param name="loggedTransferredCourse">The logged transferred course to create.</param>
        /// <returns>Whether the logged transferred course was created successfully.</returns>
        public async Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse)
        {
            _context.LoggedTransferredCourses.Add(loggedTransferredCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Deletes a logged equivalent course.</summary>
        /// <param name="id">The ID of the logged equivalent course to delete.</param>
        /// <returns>Whether the logged equivalent course was deleted.</returns>
        public async Task<bool> DeleteLoggedEquivalantCourse(Guid id)
        {
            var course = await _context.LoggedEquivalentCourses.FindAsync(id);
            if (course == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Deletes a logged transferred course.</summary>
        /// <param name="id">The ID of the logged transferred course to delete.</param>
        /// <returns>Whether the logged transferred course was deleted.</returns>
        public async Task<bool> DeleteLoggedTransferredCourse(Guid id)
        {
            var course = await _context.LoggedTransferredCourses.FindAsync(id);
            if (course == null)
                return false;

            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Gets a logged equivalent course.</summary>
        /// <param name="id">The ID of the logged equivalent course.</param>
        /// <returns>The logged equivalent course.</returns>
        public async Task<LoggedEquivalentCourse> GetLoggedEquivalantCourse(Guid id)
        {
            return await _context.LoggedEquivalentCourses.FindAsync(id);
        }

        /// <summary>Gets the logged equivalent courses.</summary>
        /// <returns>The logged equivalent courses.</returns>
        public async Task<IEnumerable<LoggedEquivalentCourse>> GetLoggedEquivalantCourses()
        {
            return await _context.LoggedEquivalentCourses.ToListAsync();
        }

        /// <summary>Gets a logged transferred course.</summary>
        /// <param name="id">The ID of the logged transferred course.</param>
        /// <returns>The logged transferred course.</returns>
        public async Task<LoggedTransferredCourse> GetLoggedTransferredCourse(Guid id)
        {
            return await _context.LoggedTransferredCourses.FindAsync(id);
        }

        /// <summary>Gets the logged transferred courses.</summary>
        /// <returns>The logged transferred courses.</returns>
        public async Task<IEnumerable<LoggedTransferredCourse>> GetLoggedTransferredCourses()
        {
            return await _context.LoggedTransferredCourses.ToListAsync();
        }

        /// <summary>Updates a logged equivalent course.</summary>
        /// <param name="loggedEquivalantCourse">The logged equivalent course to update.</param>
        /// <returns>True if the logged equivalent course was updated, false otherwise.</returns>
        public async Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse)
        {
            _context.LoggedEquivalentCourses.Update(loggedEquivalantCourse);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>Updates the logged transferred course.</summary>
        /// <param name="loggedTransferredCourse">The logged transferred course.</param>
        /// <returns>True if the logged transferred course was updated, false otherwise.</returns>
        public async Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse)
        {
            _context.LoggedTransferredCourses.Update(loggedTransferredCourse);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
