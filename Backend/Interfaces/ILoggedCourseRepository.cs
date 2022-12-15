using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface ILoggedCourseRepository
    {
        Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse);
        Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse);
        Task<LoggedEquivalentCourse> GetLoggedEquivalantCourse(Guid id);
        Task<LoggedTransferredCourse> GetLoggedTransferredCourse(Guid id);
        Task<IEnumerable<LoggedEquivalentCourse>> GetLoggedEquivalantCourses();
        Task<IEnumerable<LoggedTransferredCourse>> GetLoggedTransferredCourses();
        Task<bool> DeleteLoggedEquivalantCourse(Guid id);
        Task<bool> DeleteLoggedTransferredCourse(Guid id);
        Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalentCourse loggedEquivalantCourse);
        Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse);
    }
}
