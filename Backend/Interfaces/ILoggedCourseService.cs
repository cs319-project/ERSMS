using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface ILoggedCourseService
    {
        Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalentCourseDto loggedEquivalantCourse);
        Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse);
        Task<LoggedEquivalentCourseDto> GetLoggedEquivalantCourse(Guid id);
        Task<LoggedTransferredCourseDto> GetLoggedTransferredCourse(Guid id);
        Task<IEnumerable<LoggedEquivalentCourseDto>> GetLoggedEquivalantCourses();
        Task<IEnumerable<LoggedTransferredCourseDto>> GetLoggedTransferredCourses();
        Task<bool> DeleteLoggedEquivalantCourse(Guid id);
        Task<bool> DeleteLoggedTransferredCourse(Guid id);
        Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalentCourseDto loggedEquivalantCourse);
        Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse);
    }
}
