using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;

namespace Backend.Services
{
    /// <summary>A service for logged course operations.</summary>
    public class LoggedCourseService : ILoggedCourseService
    {
        private readonly ILoggedCourseRepository _loggedCourseRepository;
        private readonly IMapper _mapper;

        /// <summary>Initializes a new instance of the <see cref="LoggedCourseService"/> class.</summary>
        /// <param name="loggedCourseRepository">The repository for logged courses.</param>
        /// <param name="mapper">The mapper.</param>
        public LoggedCourseService(ILoggedCourseRepository loggedCourseRepository, IMapper mapper)
        {
            _loggedCourseRepository = loggedCourseRepository;
            _mapper = mapper;
        }

        /// <summary>Creates a logged equivalent course.</summary>
        /// <param name="loggedEquivalantCourse">The logged equivalent course.</param>
        /// <returns>A boolean indicating whether the logged equivalent course was created successfully.</returns>
        public async Task<bool> CreateLoggedEquivalantCourse(LoggedEquivalentCourseDto loggedEquivalantCourse)
        {
            var loggedEquivalantCourseEntity = _mapper.Map<LoggedEquivalentCourse>(loggedEquivalantCourse);

            // dont allow duplicates
            var loggedCourses = await _loggedCourseRepository.GetLoggedEquivalantCourses();
            foreach (var loggedCourse in loggedCourses)
            {
                if (loggedCourse.HostCourseCode == loggedEquivalantCourseEntity.HostCourseCode
                        && loggedCourse.ExemptedCourse.CourseCode == loggedEquivalantCourseEntity.ExemptedCourse.CourseCode)
                {
                    return false;
                }
            }

            return await _loggedCourseRepository.CreateLoggedEquivalantCourse(loggedEquivalantCourseEntity);
        }

        // This method will allow duplicates to show whether the same course has been transferred multiple times
        // for many different exchange periods

        /// <summary>Creates a logged transferred course.</summary>
        /// <param name="loggedTransferredCourse">The logged transferred course.</param>
        /// <returns>A boolean indicating whether the logged transferred course was created successfully.</returns>
        public async Task<bool> CreateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse)
        {
            var loggedTransferredCourseEntity = _mapper.Map<LoggedTransferredCourse>(loggedTransferredCourse);
            return await _loggedCourseRepository.CreateLoggedTransferredCourse(loggedTransferredCourseEntity);
        }

        /// <summary>Deletes a logged equivalant course.</summary>
        /// <param name="id">The ID of the logged equivalant course to delete.</param>
        /// <returns>A boolean indicating whether the logged equivalant course was deleted successfully.</returns>
        public async Task<bool> DeleteLoggedEquivalantCourse(Guid id)
        {
            return await _loggedCourseRepository.DeleteLoggedEquivalantCourse(id);
        }

        /// <summary>Deletes a logged transferred course.</summary>
        /// <param name="id">The ID of the logged transferred course to delete.</param>
        /// <returns>A boolean indicating whether the logged transferred course was deleted successfully.</returns>
        public async Task<bool> DeleteLoggedTransferredCourse(Guid id)
        {
            return await _loggedCourseRepository.DeleteLoggedTransferredCourse(id);
        }

        /// <summary>Gets a logged equivalant course.</summary>
        /// <param name="id">The ID of the logged equivalant course to get.</param>
        /// <returns>The logged equivalant course.</returns>
        public async Task<LoggedEquivalentCourseDto> GetLoggedEquivalantCourse(Guid id)
        {
            var loggedEquivalantCourse = await _loggedCourseRepository.GetLoggedEquivalantCourse(id);
            return _mapper.Map<LoggedEquivalentCourseDto>(loggedEquivalantCourse);
        }

        /// <summary>Gets all logged equivalant courses.</summary>
        /// <returns>All logged equivalant courses.</returns>
        public async Task<IEnumerable<LoggedEquivalentCourseDto>> GetLoggedEquivalantCourses()
        {
            var loggedEquivalantCourses = await _loggedCourseRepository.GetLoggedEquivalantCourses();
            return _mapper.Map<IEnumerable<LoggedEquivalentCourseDto>>(loggedEquivalantCourses);
        }

        /// <summary>Gets a logged transferred course.</summary>
        /// <param name="id">The ID of the logged transferred course to get.</param>
        public async Task<LoggedTransferredCourseDto> GetLoggedTransferredCourse(Guid id)
        {
            var loggedTransferredCourse = await _loggedCourseRepository.GetLoggedTransferredCourse(id);
            return _mapper.Map<LoggedTransferredCourseDto>(loggedTransferredCourse);
        }

        /// <summary>Gets all logged transferred courses.</summary>
        /// <returns>All logged transferred courses.</returns>
        public async Task<IEnumerable<LoggedTransferredCourseDto>> GetLoggedTransferredCourses()
        {
            var loggedTransferredCourses = await _loggedCourseRepository.GetLoggedTransferredCourses();
            return _mapper.Map<IEnumerable<LoggedTransferredCourseDto>>(loggedTransferredCourses);
        }

        /// <summary>Updates a logged equivalant course.</summary>
        /// <param name="loggedEquivalantCourse">The logged equivalant course to update.</param>
        public async Task<bool> UpdateLoggedEquivalantCourse(LoggedEquivalentCourseDto loggedEquivalantCourse)
        {
            return await _loggedCourseRepository.UpdateLoggedEquivalantCourse(_mapper.Map<LoggedEquivalentCourse>(loggedEquivalantCourse));
        }

        /// <summary>Updates a logged transferred course.</summary>
        /// <param name="loggedTransferredCourse">The logged transferred course to update.</param>
        public async Task<bool> UpdateLoggedTransferredCourse(LoggedTransferredCourseDto loggedTransferredCourse)
        {
            return await _loggedCourseRepository.UpdateLoggedTransferredCourse(_mapper.Map<LoggedTransferredCourse>(loggedTransferredCourse));
        }
    }
}
