using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs;

namespace Backend.Interfaces
{
    public interface ILoggedCourseService
    {
        Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalantCourseDto loggedEquivalantCourse);
        Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse);
        Task<LoggedEquivalantCourseDto> GetLoggedEquivalantCourse(Guid id);
        Task<LoggedTransferredCourseDto> GetLoggedTransferredCourse(Guid id);
        Task<IEnumerable<LoggedEquivalantCourseDto>> GetLoggedEquivalantCourses();
        Task<IEnumerable<LoggedTransferredCourseDto>> GetLoggedTransferredCourses();
        Task<bool> DeleteLoggedEquivalantCourse(Guid id);
        Task<bool> DeleteLoggedTransferredCourse(Guid id);
        Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalantCourseDto loggedEquivalantCourse);
        Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse);
    }
}
