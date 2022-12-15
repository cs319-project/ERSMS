using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Entities;

namespace Backend.Interfaces
{
    public interface ILoggedCourseRepository
    {
        Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalantCourse loggedEquivalantCourse);
        Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse);
        Task<LoggedEquivalantCourse> GetLoggedEquivalantCourse(Guid id);
        Task<LoggedTransferredCourse> GetLoggedTransferredCourse(Guid id);
        Task<IEnumerable<LoggedEquivalantCourse>> GetLoggedEquivalantCourses();
        Task<IEnumerable<LoggedTransferredCourse>> GetLoggedTransferredCourses();
        Task<bool> DeleteLoggedEquivalantCourse(Guid id);
        Task<bool> DeleteLoggedTransferredCourse(Guid id);
        Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalantCourse loggedEquivalantCourse);
        Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourse loggedTransferredCourse);
    }
}
